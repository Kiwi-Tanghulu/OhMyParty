using System.Collections.Generic;
using UnityEngine;

namespace OMG.Interiors
{
    [System.Serializable]
    public class InteriorData
    {
        private List<PlacementData> placementDatas = null;
        public PlacementData this[int index] => placementDatas[index];
        public int PlacementCount => placementDatas.Count;

        public InteriorData()
        {
            placementDatas = new List<PlacementData>();
        }

        public void AddPlacement(string propID, Vector3Int gridIndex)
        {
            placementDatas.Add(new PlacementData(propID, gridIndex));
        }
    }
}
