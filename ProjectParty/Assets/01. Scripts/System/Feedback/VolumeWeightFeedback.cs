using System;
using System.Collections;
using OMG.Editor;
using UnityEngine;
using UnityEngine.Rendering;

namespace OMG.Feedbacks
{
    public class VolumeWeightFeedback : Feedback
    {
        [System.Serializable]
        public class LoopPreset
        {
            public float From = 0f;
            public float To = 1f;
        }

        [SerializeField] Volume volume = null;
        [SerializeField] float duration = 0.5f;
        [SerializeField] float endValue = 1f;

        [Space(15f)]
        [SerializeField] bool loop = false;

        [ConditionalField("loop", true)]
        [SerializeField] LoopPreset loopSetting = new LoopPreset();

        public override void Play(Vector3 playPos)
        {
            StopAllCoroutines();

            if(loop)
                VolumeWeightLoop();
            else
                StartCoroutine(VolumeWeightRoutine(endValue, duration));
        }

        public override void Stop()
        {
            StopAllCoroutines();
            volume.weight = endValue;
        }

        private void VolumeWeightLoop()
        {
            volume.weight = loopSetting.From;
            StartCoroutine(VolumeWeightRoutine(loopSetting.To, duration, () => {
                StartCoroutine(VolumeWeightRoutine(loopSetting.From, duration, VolumeWeightLoop));
            }));
        }

        private IEnumerator VolumeWeightRoutine(float endValue, float duration, Action callback = null)
        {
            float timer = 0f;
            float startValue = volume.weight;

            while(timer <= duration)
            {
                volume.weight = Mathf.Lerp(startValue, endValue, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }

            volume.weight = endValue;
            callback?.Invoke();
        }
    }
}
