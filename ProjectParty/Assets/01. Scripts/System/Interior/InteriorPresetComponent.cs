using System.Collections.Generic;
using OMG.Datas;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorPresetComponent : MonoBehaviour
    {
        [SerializeField] PropListSO propList = null;

        private List<InteriorData> presets = null;
        private InteriorData currentInteriorData = null;

        private InteriorGridComponent gridComponent = null;
        private InteriorPlaceComponent placeComponent = null;

        private void Awake()
        {
            gridComponent = GetComponent<InteriorGridComponent>();
            placeComponent = GetComponent<InteriorPlaceComponent>();
        }

        public void Init()
        {
            presets = DataManager.UserData.InteriorPrests;
        }

        public void LoadPreset(int index)
        {
            placeComponent.ClearProps();

            currentInteriorData = presets[index];
            for(int i = 0; i < currentInteriorData.PlacementCount; ++i)
            {
                PlacementData placementData = currentInteriorData[i];
                InteriorPropSO propData = propList[placementData.PropID];
                Vector3 placePosition = gridComponent.GetGridPosition(placementData.GridIndex);
                placeComponent.PlaceProp(propData, placePosition);
            }
        }

        public void CreatePreset()
        {
            placeComponent.ClearProps();

            currentInteriorData = new InteriorData();
            presets.Add(currentInteriorData);
        }

        public void AddPlacement(string propID, Vector3Int gridIndex)
        {
            if(currentInteriorData == null)
                return;

            currentInteriorData.AddPlacement(propID, gridIndex);
        }
    }
}
