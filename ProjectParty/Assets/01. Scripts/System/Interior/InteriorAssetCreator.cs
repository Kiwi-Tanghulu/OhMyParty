using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorAssetCreator : MonoBehaviour
    {
        [SerializeField] InteriorPropType type;

        [ContextMenu("Create Data")]
        public void CreateData()
        {
            InteriorProp[] props = transform.GetComponentsInChildren<InteriorProp>();
            foreach(InteriorProp prop in props)
            {
                CreateSO(prop);
                CreatePrefabs(prop);
                LoadSOData(prop);
            }
        }

        private void CreatePrefabs(InteriorProp prop)
        {
            GameObject visual = prop.transform.Find($"{prop.gameObject.name}_Visual").gameObject;
            string path = $"Assets/02. Prefabs/Interior/{type}/{visual.name}.prefab";
            path = AssetDatabase.GenerateUniqueAssetPath(path);
            PrefabUtility.SaveAsPrefabAssetAndConnect(visual, path, InteractionMode.UserAction);

            path = $"Assets/02. Prefabs/Interior/{type}/{prop.gameObject.name}.prefab";
            path = AssetDatabase.GenerateUniqueAssetPath(path);
            PrefabUtility.SaveAsPrefabAssetAndConnect(prop.gameObject, path, InteractionMode.UserAction);
        }

        private void CreateSO(InteriorProp prop)
        {
            InteriorPropSO propData = ScriptableObject.CreateInstance<InteriorPropSO>();
            propData.name = prop.gameObject.name;

            string dataPath = $"Assets/06. SO/Interior/{type}/{propData.name}.asset";
            AssetDatabase.CreateAsset(propData, dataPath);
            AssetDatabase.Refresh();

            prop.SetPropData(propData);
        }

        private void LoadSOData(InteriorProp prop)
        {
            prop.PropData.PropType = type;

            string path = $"Assets/02. Prefabs/Interior/{type}/{prop.name}";
            prop.PropData.VisualPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{path}_Visual.prefab");
            prop.PropData.Prefab = AssetDatabase.LoadAssetAtPath<InteriorProp>($"{path}.prefab");
        }
    }
}
