using DG.Tweening;
using UnityEngine;

namespace OMG.Tweens
{
    [CreateAssetMenu(menuName = "SO/Tween/ScaleTween")]
    public class ScaleTweenSO : TweenSO
    {
        [Space(15f)]
        [SerializeField] float endValue = 1f;

        protected override void OnTween(Sequence sequence)
        {
            TweenParam param;
            Tween tween;

            for(int i = 0; i < tweenParams.Count; ++i)
            {
                param = GetParam(i);
                tween = body.DOScale(param.Value, param.Duration).SetDelay(param.Delay).SetEase(param.Ease);
                sequence.Append(tween);
            }
        }

        protected override void HandleTweenCompleted()
        {
            base.HandleTweenCompleted();
            body.localScale = new Vector3(endValue, endValue, endValue);
        }
    }
}
