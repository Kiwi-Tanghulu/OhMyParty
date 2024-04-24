using OMG.Extensions;
using OMG.Interiors;
using UnityEngine;

namespace OMG.Test
{
    public class TInterior : MonoBehaviour
    {
        [SerializeField] InteriorPropSO propData = null;
        [SerializeField] LayerMask propLayer = 0;
        [SerializeField] LayerMask groundLayer = 0;
        [SerializeField] Vector3 gridSize = Vector3.one;

        private bool enableToPlace = false;
        private Vector3 gridPosition = Vector3.zero;    

        private void Update()
        {
            if(propData == null)
                return;

            float gridSingleSize = gridSize.x;

            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            Ray groundDetectRay = Camera.main.ScreenPointToRay(mousePosition);
            if(Physics.Raycast(groundDetectRay, out RaycastHit hit, float.MaxValue, groundLayer) == false)
                return;

            Vector3 groundPosition = hit.point;
            Vector3 mod = groundPosition.GetModEach(gridSize);
            gridPosition = groundPosition - mod + mod.Sign().GetMultipleEach(gridSize.PlaneVector() * 0.5f);

            Collider[] detects = Physics.OverlapBox(gridPosition + propData.Pivot * gridSingleSize, (Vector3)propData.PropSize * gridSingleSize * 0.5f);
            enableToPlace = detects.Length < 2;
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            float gridSingleSize = gridSize.x;

            Gizmos.color = enableToPlace ? Color.green : Color.red;
            propData.DrawGizmos(gridPosition, gridSingleSize);

            int count = 100;
            Gizmos.color = Color.black;
            {
                Vector3 startPosition = new Vector3(-1000, 0, 0);
                Vector3 endPosition = new Vector3(1000, 0, 0);
                for (int i = -count / 2; i < count / 2; ++ i)
                {
                    endPosition.z = i * gridSingleSize;
                    startPosition.z = i * gridSingleSize;
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
            {
                Vector3 startPosition = new Vector3(0, 0, -1000);
                Vector3 endPosition = new Vector3(0, 0, 1000);
                for (int i = -count / 2; i < count / 2; ++ i)
                {
                    endPosition.x = i * gridSingleSize;
                    startPosition.x = i * gridSingleSize;
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
        }

        #endif
    }
}
