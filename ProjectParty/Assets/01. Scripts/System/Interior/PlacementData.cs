using UnityEngine;

namespace OMG.Interiors
{
    [System.Serializable]
    public struct PlacementData 
    {
        public string PropID;
        public Vector3Int GridIndex;

        public PlacementData(string propID, Vector3Int gridIndex)
        {
            PropID = propID;
            GridIndex = gridIndex;
        }
    }
}
