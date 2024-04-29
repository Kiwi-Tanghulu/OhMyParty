using System.Collections.Generic;
using UnityEngine;

namespace OMG.Interiors
{
    [System.Serializable]
    public class InteriorData
    {
        [SerializeField] List<PlacementData> placementDatas = null;
        public PlacementData this[int index] => placementDatas[index];
        public int PlacementCount => placementDatas.Count;

        public InteriorData()
        {
            placementDatas = new List<PlacementData>();
        }

        public PlacementData AddPlacement(string propID, Vector3Int gridIndex, int rotate)
        {
            PlacementData data = new PlacementData(propID, gridIndex, rotate);
            placementDatas.Add(data);

            return data;
        }

        public void ModifyPlacement(Vector3Int gridIndex, int rotate)
        {
            int index = placementDatas.FindIndex(i => i.GridIndex == gridIndex);
            if(index == -1)
                return;

            placementDatas[index].Rotate = rotate;
        }

        public void ClearData()
        {
            placementDatas.Clear();
        }
    }
}
