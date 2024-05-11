using DG.Tweening;
using OMG;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG
{
    public class ExtendedAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        public Animator Animator => animator;

        [HideInInspector] public AnimationEvent AnimEvent;

        private Coroutine floatParamLerpingCo;
        private Coroutine layerWeightLerpingCo;

        protected virtual void Awake()
        {
            AnimEvent = animator.GetComponent<AnimationEvent>();
        }
        
        public void SetTrigger(int hash)
        {
            animator.SetTrigger(hash);
        }

        public void SetFloat(int hash, float value, bool lerping = false, float time = 1f)
        {
            if (lerping)
            {
                if (floatParamLerpingCo != null)
                    StopCoroutine(floatParamLerpingCo);
                floatParamLerpingCo = StartCoroutine(ParamLerpingCo(hash, value, time));
            }
            else
            {
                animator.SetFloat(hash, value);
            }
        }

        public void SetInt(int hash, int value)
        {
            animator.SetInteger(hash, value);
        }

        public void SetBool(int hash, bool value)
        {
            animator.SetBool(hash, value);
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
                animator.SetLayerWeight(layerIndex, value);
            }
        }

        public void PlayAnim(string name, AnimatorLayerType layer)
        {
            animator.Play(name, (int)layer, 0f);
        }

        private IEnumerator ParamLerpingCo(int hash, float end, float time)
        {
            float start = animator.GetFloat(hash);
            float t = 0f;

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / time;
                animator.SetFloat(hash, Mathf.Lerp(start, end, t));

                yield return null;
            }
            animator.SetFloat(hash, end);
        }

        private IEnumerator LayerWeightLerpingCo(int layer, float end, float time)
        {
            float start = animator.GetLayerWeight(layer);
            float t = 0f;

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / time;
                animator.SetLayerWeight(layer, Mathf.Lerp(start, end, t));

                yield return null;
            }
            animator.SetLayerWeight(layer, end);
        }
    }
}
