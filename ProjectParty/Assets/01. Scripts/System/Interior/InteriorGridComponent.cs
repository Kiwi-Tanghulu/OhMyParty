using OMG.Inputs;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorGridComponent : MonoBehaviour
    {
        [SerializeField] LayerMask groundLayer = 0;

        private Camera mainCam = null;
        private Grid grid = null;

        public float GridSize { get; private set; } = 0f;
        public Vector3Int CurrentGridIndex { get; private set; } = Vector3Int.zero;
        public Vector3 CurrentGridPosition { get; private set; } = Vector3.zero;


        private void Awake()
        {
            mainCam = Camera.main;
            grid = GetComponent<Grid>();
        }

        public bool CalculateGrid(Vector3 placePosition)
        {
            Ray groundDetectRay = mainCam.ScreenPointToRay(placePosition);
            if(Physics.Raycast(groundDetectRay, out RaycastHit hit, float.MaxValue, groundLayer) == false)
                return false;

            CurrentGridIndex = grid.WorldToCell(hit.point);
            CurrentGridPosition = grid.GetCellCenterWorld(CurrentGridIndex);
            return true;
        }

        public void Init(float gridSize)
        {
            grid.cellSize = new Vector3(gridSize, gridSize, gridSize);
            GridSize = gridSize;
        }

        public Vector3Int GetGridIndex(Vector3 position) => grid.WorldToCell(position);
        public Vector3 GetGridPosition(Vector3 origin) => GetGridPosition(GetGridIndex(origin));
        public Vector3 GetGridPosition(Vector3Int gridIndex) => grid.GetCellCenterWorld(gridIndex);
    }
}
