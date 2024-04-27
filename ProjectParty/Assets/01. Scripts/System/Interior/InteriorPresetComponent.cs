using System.Collections.Generic;
using OMG.Datas;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorPresetComponent : MonoBehaviour
    {
        [SerializeField] PropDatabaseSO propDatabase = null;

        private UserInteriorData interiorData = null;
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
            interiorData = DataManager.UserData.InteriorData;
        }

        public void LoadPreset(int index)
        {
            interiorData.CurrentPreset = index;
            placeComponent.ClearProps();

            currentInteriorData = interiorData.InteriorPrests[interiorData.CurrentPreset];
            for(int i = 0; i < currentInteriorData.PlacementCount; ++i)
            {
                PlacementData placementData = currentInteriorData[i];
                InteriorPropSO propData = propDatabase[placementData.PropID];
                Vector3 placePosition = gridComponent.GetGridPosition(placementData.GridIndex);
                placeComponent.PlaceProp(propData, placePosition);
            }
        }

        public void ClearPreset()
        {
            placeComponent.ClearProps();
            currentInteriorData.ClearData();
        }

        public void AddPlacement(string propID, Vector3Int gridIndex)
        {
            if(currentInteriorData == null)
                return;

            currentInteriorData.AddPlacement(propID, gridIndex);
        }
    }
}
