using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class SpawnAnimationEffectFeedback : Feedback
    {
        [SerializeField] private AnimationEffect animEffect;

        public override void Play(Vector3 playPos)
        {
            AnimationEffect effect = Instantiate(animEffect, playPos, Quaternion.identity);
        }
    }
}