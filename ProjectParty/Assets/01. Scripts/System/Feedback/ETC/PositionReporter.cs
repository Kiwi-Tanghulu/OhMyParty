using UnityEngine;
using UnityEngine.Events;

namespace OMG.Feedbacks
{
    public class PositionReporter : MonoBehaviour
    {
        [SerializeField] Transform reportBody = null;
        [SerializeField] float distance = 5f;

        [Space(15f)]
        [SerializeField] UnityEvent OnPositionChangedEvent = null;

        private Vector3 lastPosition = Vector3.zero;

        private void Awake()
        {
            if(reportBody == null)
                reportBody = transform;
        }

        private void Start()
        {
            lastPosition = transform.position;
        }

        private void Update()
        {
            Vector3 distanceVector = reportBody.position - lastPosition;
            if(distanceVector.sqrMagnitude < distance * distance)
                return;

            OnPositionChangedEvent?.Invoke();
            lastPosition = reportBody.position;
        }
    }
}
