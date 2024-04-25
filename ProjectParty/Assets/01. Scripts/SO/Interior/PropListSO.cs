using System.Collections.Generic;
using UnityEngine;

namespace OMG.Interiors
{
    [CreateAssetMenu(menuName = "SO/Interior/PropList")]
    public class PropListSO : ScriptableObject
    {
        [SerializeField] List<InteriorPropSO> propDatas = new List<InteriorPropSO>();

        private Dictionary<string, InteriorPropSO> propDictionary = new Dictionary<string, InteriorPropSO>();
        public InteriorPropSO this[string propID] => propDictionary[propID];

        private void OnValidate()
        {
            propDictionary = new Dictionary<string, InteriorPropSO>();

            for(int i = 0; i < propDatas.Count; ++i)
            {
                InteriorPropSO propData = propDatas[i];
                if(propData == null)
                    return;
                propDictionary.Add(propData.PropID, propData);
            }

            Debug.Log($"{propDictionary.Count} Prop Datas Stored in Dictionary");
        }
    }
}
