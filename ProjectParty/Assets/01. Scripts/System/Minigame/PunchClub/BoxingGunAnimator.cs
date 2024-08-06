using System;
using UnityEngine;

namespace OMG.Minigames.PunchClub
{
    public class BoxingGunAnimator : MonoBehaviour
    {
        public event Action OnAnimationTriggerEvent = null;

        private Animator animator = null;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetFire()
        {
            animator.SetTrigger("Fire");
        }

        public void OnTriggerEvent()
        {
            OnAnimationTriggerEvent?.Invoke();
        }
    }
}
