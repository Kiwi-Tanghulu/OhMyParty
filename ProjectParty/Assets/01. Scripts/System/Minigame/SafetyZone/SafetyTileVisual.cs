using System;
using TMPro;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileVisual : MonoBehaviour
    {
        private Animator animator = null;
        private TMP_Text numberText = null;

        private Action callbackCache = null;

        private void Awake()
        {
            numberText = transform.Find("NumberText").GetComponent<TMP_Text>();
            animator = GetComponent<Animator>();
        }

        public void SetNumberText(int number)
        {
            if(number == -1)
                numberText.text = "-";
            else
                numberText.text = number.ToString();
        }

        public void Appear(Action callback = null)
        {
            animator.SetBool("Appear", true);
            callbackCache = callback;
        }

        public void Disappear(Action callback = null)
        {
            animator.SetBool("Disappear", true);
            callbackCache = callback;
        }

        private void ClearAnimation()
        {
            animator.SetBool("Disappear", false);
            animator.SetBool("Appear", false);
        }

        public void OnAnimationEvent()
        {
            ClearAnimation();

            Action callback = callbackCache;
            callbackCache = null;
            callback?.Invoke();
        }
    }
}
