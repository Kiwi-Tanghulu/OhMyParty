using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorProp : MonoBehaviour
    {
        [SerializeField] float gridSize = 1.5f;

        [SerializeField] InteriorPropSO propData = null;
        public InteriorPropSO PropData => propData;

        private void Update()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                Collider[] detects = Physics.OverlapBox(transform.position + propData.Pivot * gridSize, (Vector3)propData.PropSize * gridSize * 0.5f);
                Debug.Log(detects.Length);
            }
        }

        // #if UNITY_EDITOR

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     propData.DrawGizmos(transform.position, gridSize);
        // }

        // #endif
    }
}
