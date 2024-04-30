using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorAssetCreator : MonoBehaviour
    {
        [SerializeField] InteriorPropType type;

#if UNITY_EDITOR
        [ContextMenu("Create Data")]
        public void CreateData()
        {
            InteriorProp[] props = transform.GetComponentsInChildren<InteriorProp>();
            foreach(InteriorProp prop in props)
            {
                CreatePrefabs(prop);
                InteriorPropSO data = CreateSO(prop);
                data.FillData();
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

        private InteriorPropSO CreateSO(InteriorProp prop)
        {
            InteriorPropSO propData = ScriptableObject.CreateInstance<InteriorPropSO>();
            propData.name = prop.gameObject.name;
            propData.PropType = type;

            string dataPath = $"Assets/06. SO/Interior/{type}/{propData.name}.asset";
            AssetDatabase.CreateAsset(propData, dataPath);
            AssetDatabase.Refresh();

            return propData;
        }
#endif
    }
}
