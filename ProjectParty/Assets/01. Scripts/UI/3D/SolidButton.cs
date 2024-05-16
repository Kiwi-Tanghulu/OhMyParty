using OMG.Tweens;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI.Solid
{
    public class SolidButton : SolidUI, IPointerDown, IPointerUp, IPointerEnter, IPointerExit
    {
        [SerializeField] TweenOptOption focusTweenOption;
        [SerializeField] TweenOptOption clickTweenOption;

        [Space(15f)]
        public UnityEvent OnClickEvent = null;

        private void Awake()
        {
            focusTweenOption.Init(transform);
            clickTweenOption.Init(transform);
        }

        public void PointerDownEvent(Vector3 point)
        {
            clickTweenOption.PlayPositiveTween();
            OnClickEvent?.Invoke();
        }

        public void PointerUpEvent(Vector3 point)
        {
            clickTweenOption.PlayNegativeTween();
        }

        public void PointerEnterEvent(Vector3 point)
        {
            focusTweenOption.PlayPositiveTween();
        }

        public void PointerExitEvent(Vector3 point)
        {
            focusTweenOption.PlayNegativeTween();
        }
    }
}
