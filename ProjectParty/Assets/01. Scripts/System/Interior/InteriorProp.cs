using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorProp : MonoBehaviour
    {
        [SerializeField] InteriorPropSO propData = null;
        public InteriorPropSO PropData => propData;

        public void SetPropData(InteriorPropSO propData)
        {
            this.propData = propData;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(propData != null)
                propData.DrawGizmos(transform.position, 1f);
        }
        #endif
    }
}
