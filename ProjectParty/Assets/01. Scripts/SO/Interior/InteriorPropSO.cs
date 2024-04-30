using OMG.Extensions;
using UnityEditor;
using UnityEngine;

namespace OMG.Interiors
{
    [CreateAssetMenu(menuName = "SO/Interior/PropData")]
    public class InteriorPropSO : ScriptableObject
    {
        public InteriorPropType PropType = InteriorPropType.Furniture;
        public string PropID = "";

        public InteriorProp Prefab = null;
        public GameObject VisualPrefab = null;
        public Vector3Int PropSize = Vector3Int.one;

        [SerializeField] Vector3Int offset = Vector3Int.zero;
        public Vector3Int Offset {
            get => offset;
            set {
                offset = value.GetClampEach(-(PropSize - Vector3Int.one), Vector3Int.zero);
            }
        }

        public Vector3 Center => offset + (Vector3)(PropSize - Vector3Int.one) * 0.5f;

        #if UNITY_EDITOR
        [ContextMenu("Fill Data")]
        public void FillData()
        {
            OnValidate();
            string path = $"Assets/02. Prefabs/Interior/{PropType}/{name}";
            VisualPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{path}_Visual.prefab");
            Prefab = AssetDatabase.LoadAssetAtPath<InteriorProp>($"{path}.prefab");
            Prefab.SetPropData(this);
        }

        private void OnValidate()
        {
            Offset = offset;

            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            PropID = UnityEditor.AssetDatabase.AssetPathToGUID(path);
        }

        [SerializeField] bool drawGizmos = false;
        public void DrawGizmos(Vector3 position, float gridSize)
        {
            if(drawGizmos == false)
                return;

            Vector3 size = (Vector3)PropSize * gridSize;
            Vector3 center = position + Center;

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(center, size);
        }
        #endif
    }
}
