using OMG.Extensions;
using UnityEngine;

namespace OMG.Interiors
{
    [CreateAssetMenu(menuName = "SO/Interior/PropData")]
    public class InteriorPropSO : ScriptableObject
    {
        public Mesh PropMesh = null;
        public Vector3Int PropSize = Vector3Int.one;

        [SerializeField] Vector3Int offset = Vector3Int.zero;
        public Vector3Int Offset {
            get => offset;
            set {
                offset = value.GetClampEach(-(PropSize - Vector3Int.one), Vector3Int.zero);
            }
        }

        public Vector3 Pivot => offset + (Vector3)(PropSize - Vector3Int.one) * 0.5f;

        private void OnValidate()
        {
            Offset = offset;
        }

        [SerializeField] bool drawGizmos = false;
        public void DrawGizmos(Vector3 position, float gridSize)
        {
            if(drawGizmos == false)
                return;

            Vector3 size = (Vector3)PropSize * gridSize;
            Vector3 center = position + Pivot * gridSize;

            Gizmos.DrawWireCube(center, size);
        }
    }
}
