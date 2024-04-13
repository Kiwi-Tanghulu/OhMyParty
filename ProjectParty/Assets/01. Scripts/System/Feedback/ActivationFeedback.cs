using OMG.Extensions;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class ActivationFeedback : Feedback
    {
        [SerializeField] GameObject targetObject = null;
        [SerializeField] float turnOffTime = 1f;

        public override void Play(Transform playTrm)
        {
            targetObject.SetActive(true);
            StartCoroutine(this.DelayCoroutine(turnOffTime, () => targetObject.SetActive(false)));
        }
    }
}
