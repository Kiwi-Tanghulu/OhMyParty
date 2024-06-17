using OMG.Extensions;
using UnityEngine;

namespace OMG.ETC
{
    public class IntroPlayerAnimator : MonoBehaviour
    {
        [SerializeField] int typeCount = 4;
        [SerializeField] float delay = 1f;
        [SerializeField] float randomness = 0.95f;

        private Animator animator = null;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            ReserveAction();
        }

        public void OnAnimationEnd()
        {
            animator.SetBool("IsAction", false);
            ReserveAction();
        }

        private void ReserveAction()
        {
            float delay = this.delay + Random.Range(-randomness, randomness);
            StartCoroutine(this.DelayCoroutine(delay, SetAction));
        }

        private void SetAction()
        {
            int actionType = Random.Range(0, typeCount);
            animator.SetFloat("ActionType", actionType);
            animator.SetBool("IsAction", true);
        }
    }
}
