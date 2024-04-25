using System.Collections.Generic;
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
    }
}