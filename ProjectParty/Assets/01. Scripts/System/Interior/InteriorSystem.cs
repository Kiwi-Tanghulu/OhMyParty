using System;
using OMG.Input;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorSystem : MonoBehaviour
    {
        [SerializeField] InteriorInputSO input = null;
        
        [Space(15f)]
        [SerializeField] LayerMask obstacleLayer = 0;
        [SerializeField] LayerMask groundLayer = 0;
        [SerializeField] float gridSize = 1f;
        public float GridSize => gridSize;

        private InteriorPropSO currentPropData = null;
        private Camera mainCam = null;
        private Grid grid = null;

        private bool isActive = false;
        private bool enableToPlace = false;

        private Collider[] obstacleCache = new Collider[1];
        private Vector3 gridPositionCache = Vector3.zero;

        private void Awake()
        {
            mainCam = Camera.main;

            grid = GetComponent<Grid>();
            grid.cellSize = new Vector3(gridSize, gridSize, gridSize);
        }

        private void Update()
        {
            if(isActive == false)
                return;

            Ray groundDetectRay = mainCam.ScreenPointToRay(input.PlacePosition);
            if(Physics.Raycast(groundDetectRay, out RaycastHit hit, float.MaxValue, groundLayer) == false)
                return;

            Vector3Int gridIndex = grid.WorldToCell(hit.point);
            Vector3 gridPosition = grid.GetCellCenterWorld(gridIndex);
            gridPositionCache = gridPosition;

            Vector3 center = gridPosition + currentPropData.Pivot * gridSize;
            Vector3 halfExtents = (Vector3)currentPropData.PropSize * gridSize * 0.45f;
            int detectedCount = Physics.OverlapBoxNonAlloc(center, halfExtents, obstacleCache, Quaternion.identity, obstacleLayer);
            enableToPlace = detectedCount < 1;
        }

        public void SetPropData(InteriorPropSO propData)
        {
            currentPropData = propData;
            isActive = true;
            enableToPlace = false;

            RegisterHandleInput();
        }

        public void ClearProp()
        {
            UnregisterHandleInput();

            currentPropData = null;
            isActive = false;
            enableToPlace = false;
        }

        private void RegisterHandleInput()
        {
            input.OnPlaceEvent += HandlePlace;
        }

        private void UnregisterHandleInput()
        {
            input.OnPlaceEvent -= HandlePlace;
        }

        private void HandlePlace()
        {
            if(enableToPlace == false)
                return;

            Instantiate(currentPropData.Prefab, gridPositionCache, Quaternion.identity);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = enableToPlace ? Color.green : Color.red;
            currentPropData?.DrawGizmos(gridPositionCache, gridSize);
        }
        #endif
    }
}
