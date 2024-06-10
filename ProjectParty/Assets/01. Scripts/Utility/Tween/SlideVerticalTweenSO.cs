using DG.Tweening;
using UnityEngine;

namespace OMG.Tweens
{
    [CreateAssetMenu(menuName = "SO/Tween/SlideVerticalTween")]
    public class SlideVerticalTweenSO : TweenSO
    {
        [Space(15f)]
        [SerializeField] float endValue = 0f;

        protected override void OnTween(Sequence sequence)
        {
            TweenParam param;
            Tween tween;

            for (int i = 0; i < tweenParams.Count; ++i)
            {
                param = GetParam(i);
                tween = body.DOLocalMoveY(param.Value, param.Duration).SetDelay(param.Delay).SetEase(param.Ease);
                sequence.Append(tween);
            }
        }

        protected override void HandleTweenCompleted()
        {
            base.HandleTweenCompleted();
            body.localPosition = new Vector3(body.localPosition.x, endValue, body.localPosition.z);
        }

        protected override void HandleTweenForceKilled()
        {
            base.HandleTweenForceKilled();
            body.localPosition = new Vector3(body.localPosition.x, endValue, body.localPosition.z);
        }
    }
}