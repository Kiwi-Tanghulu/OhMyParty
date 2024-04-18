using DG.Tweening;
using UnityEngine;

namespace OMG.Tweens
{
    [CreateAssetMenu(menuName = "SO/Tween/ShakeTween")]
    public class ShakeTweenSO : TweenSO
    {
        [Space(15f)]
        [SerializeField] float strength = 1f;
        [SerializeField] int vibrato = 10;

        protected override void OnTween(Sequence sequence)
        {
            Tween tween;
            tween = body.DOShakePosition(GetParam(0).Duration, strength, vibrato);
            sequence.Append(tween);
        }
    }
}