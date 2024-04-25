using OMG.Input;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorSystem : MonoBehaviour
    {
        [SerializeField] InteriorInputSO input = null;
        [SerializeField] PropListSO propList = null;
        [SerializeField] float gridSize = 1f;
        public float GridSize => gridSize;

        private InteriorData interiorData = null;
        private InteriorPropSO currentPropData = null;
        private bool enableToPlace = false;
        private bool active = false;

        private InteriorGridComponent gridComponent = null;
        private InteriorPlaceComponent placeComponent = null;

        private void Awake()
        {
            interiorData = new InteriorData();
            gridComponent = GetComponent<InteriorGridComponent>();
            placeComponent = GetComponent<InteriorPlaceComponent>();
        }

        private void Start()
        {
            gridComponent.Init(gridSize);
        }

        private void Update()
        {
            if(active == false)
                return;

            gridComponent.CalculateGrid(input.PlacePosition);
            enableToPlace = placeComponent.EnableToPlace(currentPropData, gridComponent.CurrentGridPosition, gridSize);
        }

        public void SetPropData(string propID)
        {
            input.OnPlaceEvent += HandlePlace;
            currentPropData = propList[propID];
            active = true;
        }

        public void ClearPropData()
        {
            input.OnPlaceEvent -= HandlePlace;
            currentPropData = null;
            active = false;
        }

        private void HandlePlace()
        {
            if(active == false)
                return;

            if(enableToPlace == false)
                return;

            placeComponent.PlaceProp(currentPropData, gridComponent.CurrentGridPosition);
            interiorData.AddPlacement(currentPropData.PropID, gridComponent.CurrentGridIndex);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = enableToPlace ? Color.green : Color.red;
            currentPropData?.DrawGizmos(gridComponent.CurrentGridPosition, gridSize);
        }
        #endif
    }
}
