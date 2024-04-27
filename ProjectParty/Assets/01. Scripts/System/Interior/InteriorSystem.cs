using OMG.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OMG.Interiors
{
    public class InteriorSystem : MonoBehaviour
    {
        [SerializeField] InteriorInputSO input = null;
        [SerializeField] PropDatabaseSO propDatabase = null;
        [SerializeField] float gridSize = 1f;
        public float GridSize => gridSize;

        private InteriorPropSO currentPropData = null;
        private bool enableToPlace = false;
        private bool active = false;

        private InteriorPresetComponent presetComponent = null;
        private InteriorGridComponent gridComponent = null;
        private InteriorPlaceComponent placeComponent = null;
        private InteriorVisualComponent visualComponent = null;

        private EventSystem eventSystem = null;

        private void Awake()
        {
            presetComponent = GetComponent<InteriorPresetComponent>();
            gridComponent = GetComponent<InteriorGridComponent>();
            placeComponent = GetComponent<InteriorPlaceComponent>();
            visualComponent = GetComponent<InteriorVisualComponent>();

            eventSystem = EventSystem.current;
        }

        private void Start()
        {
            presetComponent.Init();
            gridComponent.Init(gridSize);
            visualComponent.Init(gridSize);
        }

        private void Update()
        {
            if(active == false)
                return;

            if(eventSystem.IsPointerOverGameObject())
            {
                enableToPlace = false;
                visualComponent.UpdateBound(gridComponent.CurrentGridPosition, enableToPlace);
                return;
            }

            enableToPlace = gridComponent.CalculateGrid(input.PlacePosition);
            if(enableToPlace)
                enableToPlace = placeComponent.EnableToPlace(currentPropData, gridComponent.CurrentGridPosition, gridSize);
            visualComponent.UpdateBound(gridComponent.CurrentGridPosition, enableToPlace);
        }

        public void SetPropData(string propID)
        {
            input.OnPlaceEvent += HandlePlace;
            currentPropData = propDatabase[propID];
            visualComponent.SetPropBound(currentPropData);
            visualComponent.Display(true);
            active = true;
        }

        public void ClearPropData()
        {
            input.OnPlaceEvent -= HandlePlace;
            visualComponent.Display(false);
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
            presetComponent.AddPlacement(currentPropData, gridComponent.CurrentGridIndex);
        }
    }
}
