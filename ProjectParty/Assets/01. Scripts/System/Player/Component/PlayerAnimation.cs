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
        private Coroutine layerWeightLerpingCo;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void SetTrigger(int hash)
        {
            anim.SetTrigger(hash);
        }

        public void SetFloat(int hash, float value, bool lerping = false, float time = 1f)
        {
            if(lerping)
            {
                if(paramLerpingCo != null)
                    StopCoroutine(paramLerpingCo);
                paramLerpingCo = StartCoroutine(ParamLerpingCo(hash, value, time));
            }
            else
            {
                anim.SetFloat(hash, value);
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

        public void SetLayerWeight(AnimatorLayerType layer, float value, bool lerping = false, float time = 1f)
        {
            int layerIndex = (int)layer;

            if (lerping)
            {
                if (layerWeightLerpingCo != null)
                    StopCoroutine(layerWeightLerpingCo);
                layerWeightLerpingCo = StartCoroutine(LayerWeightLerpingCo(layerIndex, value, time));
            }
            else
            {
                anim.SetLayerWeight(layerIndex, value);
            }
        }

        public void PlayAnim(string name, AnimatorLayerType layer)
        {
            anim.Play(name, (int)layer, 0f);
        }

        public void InvokeStartEvent() => OnStartEvent?.Invoke();   
        public void InvokePlayingEvent() => OnPlayingEvent?.Invoke();   
        public void InvokeEndEvent() => OnEndEvent?.Invoke();

        private IEnumerator ParamLerpingCo(int hash, float end, float time)
        {
            float start = anim.GetFloat(hash);
            float t = 0f;

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / time;
                anim.SetFloat(hash, Mathf.Lerp(start, end, t));

                yield return null;
            }
            anim.SetFloat(hash, end);
        }

        private IEnumerator LayerWeightLerpingCo(int layer, float end, float time)
        {
            float start = anim.GetLayerWeight(layer);
            float t = 0f;

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / time;
                anim.SetLayerWeight(layer, Mathf.Lerp(start, end, t));

                yield return null;
            }
            anim.SetLayerWeight(layer, end);
        }
    }
}
