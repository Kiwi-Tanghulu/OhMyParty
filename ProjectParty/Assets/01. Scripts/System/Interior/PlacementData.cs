using UnityEngine;

namespace OMG.Interiors
{
    [System.Serializable]
    public struct PlacementData 
    {
        public string PropID;
        public Vector3Int GridIndex;
        public int Rotate;

        public PlacementData(string propID, Vector3Int gridIndex,int rotate)
        {
            PropID = propID;
            GridIndex = gridIndex;
            Rotate = rotate;
        }
    }
}
