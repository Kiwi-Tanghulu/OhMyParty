using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class FadeUI : MonoBehaviour
    {
        private Animator anim;

        private readonly int fadeInHash = Animator.StringToHash("fadeIn");
        private readonly int fadeOutHash = Animator.StringToHash("fadeOut");

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void FadeIn()
        {
            anim.SetTrigger(fadeInHash);
        }

        public void FadeOut()
        {
            anim.SetTrigger(fadeOutHash);
        }
    }
}