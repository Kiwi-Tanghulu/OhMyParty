using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorProp : MonoBehaviour
    {
        [SerializeField] InteriorPropSO propData = null;
        public InteriorPropSO PropData => propData;
    }
}
