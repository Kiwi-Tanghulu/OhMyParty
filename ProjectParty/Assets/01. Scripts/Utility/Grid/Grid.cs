using OMG.Extensions;
using UnityEngine;

namespace OMG.Grids
{
    public class Grid
    {
        private Vector3 gridSize = Vector3.zero;
        private float gridSingleSize = 0f;

        public Grid(float size)
        {
            gridSingleSize = size;
            gridSize = new Vector3(gridSingleSize, gridSingleSize, gridSingleSize);
        }

        public Vector3Int GetGridIndex(Vector3 position)
        {
            Vector3 mod = position.GetModEach(gridSize);
            Vector3 gridIndex = (position - mod) / gridSingleSize;

            return gridIndex.GetRoundEach();
        }

        public Vector3 GetPositionByIndex(Vector3 index)
        {
            Vector3 position = index * gridSingleSize;
            position += position.Sign(true) * gridSingleSize * 0.5f;
            return position;
        }
    }
}
