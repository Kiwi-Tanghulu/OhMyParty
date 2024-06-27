using OMG.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class AnimationEffect : Effect
    {
        private Animator anim;
        public Animator Anim => anim;

        private float playTime;

        public override void Awake()
        {
            base.Awake();

            anim = GetComponent<Animator>();

            playTime = anim.runtimeAnimatorController.animationClips[0].length;

            StartCoroutine(this.DelayCoroutine(playTime, () => Destroy(gameObject)));
        }
    }
}
