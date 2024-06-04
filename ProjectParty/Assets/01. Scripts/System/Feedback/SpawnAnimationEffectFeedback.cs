using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class SpawnAnimationEffectFeedback : Feedback
    {
        [SerializeField] private Animator animEffect;
        private WaitForSeconds wfs;

        private void Awake()
        {
            float playTime = animEffect.runtimeAnimatorController.animationClips[0].length;
            wfs = new WaitForSeconds(playTime);
        }

        public override void Play(Transform playTrm)
        {
            Animator effect = Instantiate(animEffect, playTrm.position, Quaternion.identity);
            StartCoroutine(DestoryEffect(effect));
        }

        private IEnumerator DestoryEffect(Animator effect)
        {
            yield return wfs;

            Destroy(effect.gameObject);
        }
    }
}