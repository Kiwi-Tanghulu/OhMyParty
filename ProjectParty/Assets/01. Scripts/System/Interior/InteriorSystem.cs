using System;
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
        private int rotate = 0;

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
            if(currentPropData != null)
                ClearPropData();

            InputManager.ChangeInputMap(InputMapType.Interior);

            rotate = 0;
            visualComponent.ResetBound();

            input.OnPlaceEvent += HandlePlace;
            input.OnRotateEvent += HandleRotate;
            input.OnCancelEvent += HandleCancel;
            
            currentPropData = propDatabase[propID];
            visualComponent.SetPropBound(currentPropData);
            visualComponent.Display(true);
            active = true;
        }

        public void ClearPropData()
        {
            InputManager.ChangeInputMap(InputMapType.UI);

            input.OnPlaceEvent -= HandlePlace;
            input.OnRotateEvent -= HandleRotate;
            input.OnCancelEvent -= HandleCancel;

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

            placeComponent.PlaceProp(currentPropData, gridComponent.CurrentGridPosition, rotate);
            presetComponent.AddPlacement(currentPropData, gridComponent.CurrentGridIndex, rotate);
        }

        private void HandleRotate(int direction)
        {
            int prev = rotate;
            rotate = (rotate + 4 + direction) % 4;

            visualComponent.SetRotate(rotate - prev);
        }

        private void HandleCancel()
        {
            if(currentPropData != null)
                ClearPropData();
        }
    }
}
