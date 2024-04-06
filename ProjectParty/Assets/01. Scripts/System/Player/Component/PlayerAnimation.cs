using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator anim;

        public event Action OnStartEvent;
        public event Action OnPlayingEvent;
        public event Action OnEndEvent;

        private Coroutine paramLerpingCo;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void SetTrigger(int hash)
        {
            anim.SetTrigger(hash);
        }

        public void SetFloat(int hash, float value, bool lerping = false)
        {
            if(lerping)
            {
                anim.SetFloat(hash, value);
            }
            else
            {

            }
        }

        public void SetInt(int hash, int value)
        {
            anim.SetInteger(hash, value);
        }

        public void SetBool(int hash, bool value)
        {
            anim.SetBool(hash, value);
        }

        public void InvokeStartEvent() => OnStartEvent?.Invoke();   
        public void InvokePlayingEvent() => OnPlayingEvent?.Invoke();   
        public void InvokeEndEvent() => OnEndEvent?.Invoke();   

        private IEnumerator ParamLerpingCo(float dest)
        {
            yield return null;
        }
    }
}
