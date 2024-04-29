using OMG.Datas;
using Unity.Netcode;
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
                InteriorProp prop = placeComponent.PlaceProp(propData, placementData.GridIndex, placePosition, placementData.Rotate);
                prop.Init(placementData);
            }
        }

        public void ClearPreset()
        {
            placeComponent.ClearProps();
            currentInteriorData?.ClearData();
        }

        public PlacementData AddPlacement(InteriorPropSO propData, Vector3Int gridIndex, int rotate)
        {
            if(currentInteriorData == null)
                return default;

            return currentInteriorData.AddPlacement(propData.PropID, gridIndex, rotate);
        }

        public void RemovePlacement(InteriorProp prop)
        {
            currentInteriorData.RemovePlacement(prop.PlacementData.GridIndex);
        }

        public void ModifyPlacement(Vector3Int gridIndex, int rotate)
        {
            currentInteriorData.ModifyPlacement(gridIndex, rotate);
        }
    }
}
