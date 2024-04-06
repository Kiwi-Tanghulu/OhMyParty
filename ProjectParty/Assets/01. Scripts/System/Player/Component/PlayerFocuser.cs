using OMG.Interacting;
using UnityEngine;
using UnityEngine.UIElements;

namespace OMG.Player
{
    public class PlayerFocuser : MonoBehaviour
    {
        [SerializeField] Transform eyeTranform = null;
        [SerializeField] LayerMask focusingLayer = 0;
        [SerializeField] float distance = 1.5f;
        [SerializeField] float radius = 0.5f;

        private IFocusable focusedObject = null;
        public IFocusable FocusedObject => focusedObject;

        public bool IsEmpty => focusedObject == null;

        public Vector3 FocusedPoint = Vector3.zero;

        private void Awake()
        {
            //eyeTranform = Camera.main.transform;
        }

        private void Update()
        {
            IFocusable other = null;
            Vector3 point = eyeTranform.position + eyeTranform.forward * distance;

            //bool rayResult = Physics.Raycast(eyeTranform.position, eyeTranform.forward, out RaycastHit hit, distance, focusingLayer);
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, focusingLayer);
            if (cols.Length > 0)
            {
                //hit.collider.TryGetComponent<IFocusable>(out other);
                //point = hit.point;

                cols[0].TryGetComponent<IFocusable>(out other);
                point = cols[0].transform.position;
            }

            if (focusedObject != other)
                FocusObject(other, point);

            FocusedPoint = point;
        }

        private void FocusObject(IFocusable other, Vector3 point)
        {
            focusedObject?.OnFocusEnd();
            focusedObject = other;
            focusedObject?.OnFocusBegin(point);
        }

#if UNITY_EDITOR
        [Space(15f)]
        [SerializeField] bool gizmo = false;

        private void OnDrawGizmos()
        {
            if (gizmo == false)
                return;

            if (eyeTranform == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }

}