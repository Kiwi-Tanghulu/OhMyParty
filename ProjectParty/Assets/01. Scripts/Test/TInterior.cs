using OMG.Extensions;
using OMG.Interiors;
using UnityEngine;

namespace OMG.Test
{
    public class TInterior : MonoBehaviour
    {
        [SerializeField] InteriorPropSO propData = null;
        [SerializeField] LayerMask obstacleLayer = 0;
        [SerializeField] LayerMask groundLayer = 0;
        [SerializeField] float gridSize = 1f;

        private bool enableToPlace = false;
        private Vector3 gridPosition = Vector3.zero;    

        private Grid grid = null;

        private void Awake()
        {
            grid = GetComponent<Grid>();
            grid.cellSize = new Vector3(gridSize, gridSize, gridSize);
        }

        private void Update()
        {
            if(propData == null)
                return;

            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            Ray groundDetectRay = Camera.main.ScreenPointToRay(mousePosition);
            if(Physics.Raycast(groundDetectRay, out RaycastHit hit, float.MaxValue, groundLayer) == false)
                return;

            Vector3Int gridIndex = grid.WorldToCell(hit.point);
            gridPosition = grid.GetCellCenterWorld(gridIndex);

            Collider[] detects = Physics.OverlapBox(gridPosition + propData.Pivot * gridSize, (Vector3)propData.PropSize * gridSize * 0.5f * 0.95f, Quaternion.identity, obstacleLayer);
            enableToPlace = detects.Length < 1;
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = enableToPlace ? Color.green : Color.red;
            propData.DrawGizmos(gridPosition, gridSize);

            int count = 100;
            Gizmos.color = Color.black;
            {
                Vector3 startPosition = new Vector3(-1000, 0, 0);
                Vector3 endPosition = new Vector3(1000, 0, 0);
                for (int i = -count / 2; i < count / 2; ++ i)
                {
                    endPosition.z = i * gridSize;
                    startPosition.z = i * gridSize;
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
            {
                Vector3 startPosition = new Vector3(0, 0, -1000);
                Vector3 endPosition = new Vector3(0, 0, 1000);
                for (int i = -count / 2; i < count / 2; ++ i)
                {
                    endPosition.x = i * gridSize;
                    startPosition.x = i * gridSize;
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
        }

        #endif
    }
}
