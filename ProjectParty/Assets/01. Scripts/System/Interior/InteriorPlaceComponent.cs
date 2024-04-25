using System.Collections.Generic;
using OMG.Input;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorPlaceComponent : MonoBehaviour
    {
        [SerializeField] LayerMask obstacleLayer = 0;

        private List<InteriorProp> props = null;
        private Collider[] obstacleCache = new Collider[1];

        private void Awake()
        {
            props = new List<InteriorProp>();
        }

        public bool EnableToPlace(InteriorPropSO propData, Vector3 gridPosition, float gridSize)
        {
            Vector3 center = gridPosition + propData.Pivot * gridSize;
            Vector3 halfExtents = (Vector3)propData.PropSize * gridSize * 0.45f;
            int detectedCount = Physics.OverlapBoxNonAlloc(center, halfExtents, obstacleCache, Quaternion.identity, obstacleLayer);
            return detectedCount < 1;
        }

        public void PlaceProp(InteriorPropSO propData, Vector3 position)
        {
            props.Add(Instantiate(propData.Prefab, position, Quaternion.identity));
        }

        public void ClearProps()
        {
            props.ForEach(i => {
                Destroy(i?.gameObject);
            });

            props.Clear();
        }
    }
}
