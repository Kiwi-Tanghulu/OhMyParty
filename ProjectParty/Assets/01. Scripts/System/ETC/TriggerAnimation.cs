using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Animation
{
    public class TriggerAnimation : MonoBehaviour
    {
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void Play()
        {
            anim.SetTrigger("trigger"); 
        }
    }
}