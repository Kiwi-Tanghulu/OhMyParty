using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OMG.Interiors
{
    [CreateAssetMenu(menuName = "SO/Interior/PropList")]
    public class PropListSO : ScriptableObject
    {
        public InteriorPropType PropType = InteriorPropType.Furniture;
        [SerializeField] List<InteriorPropSO> propDatas = new List<InteriorPropSO>();
        public InteriorPropSO this[int index] => propDatas[index];
        public int Count => propDatas.Count;

        private void OnValidate()
        {
            for(int i = 0; i < propDatas.Count; ++i)
            {
                if(propDatas[i] == null)
                {
                    propDatas.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}