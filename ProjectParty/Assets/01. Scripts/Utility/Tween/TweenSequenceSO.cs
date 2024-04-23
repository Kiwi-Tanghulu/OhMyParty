using System.Collections.Generic;
using DG.Tweening;
using OMG.Extensions;
using UnityEngine;

namespace OMG.Tweens
{
    [CreateAssetMenu(menuName = "SO/Tween/Sequence")]
    public class TweenSequenceSO : TweenSO
    {
        [SerializeField] TweenSO[] tweenSequence = null; 

        public override TweenSO CreateInstance(Transform body)
        {
            TweenSequenceSO instance = ScriptableObject.Instantiate(this);

            instance.tweenSequence.ForEach((i, index) => {
                instance.tweenSequence[index] = i.CreateInstance(body);
            });

            return instance;
        }

        protected override void OnTween(Sequence sequence)
        {
            tweenSequence.ForEach(i => {
                sequence.Append(i.CreateTween());
            });
        }
    }
}
