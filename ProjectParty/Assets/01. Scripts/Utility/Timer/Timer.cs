using System;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Timers
{
    public class Timer : MonoBehaviour
    {
        /// <summary>
        /// (ratio, single)
        /// </summary> 
        public UnityEvent<float, float> OnValueChangedEvent = new UnityEvent<float, float>();
        public UnityEvent OnTimerFinishedEvent = new UnityEvent();

        private Action callbackCache = null;

        private float timer = 0f;
        private float time = 0f;

        public float Ratio => timer / time;
        public bool Finished => timer <= 0f;

        protected virtual void Update()
        {
            if(Finished)
                return;

            timer -= Time.deltaTime;
            OnValueChangedEvent?.Invoke(Ratio, timer);
            if(Finished)
            {
                OnTimerFinishedEvent?.Invoke();
                HandleCallback();
            }
        }

        private void HandleCallback()
        {
            Action callback = callbackCache;
            callbackCache = null;
            callback?.Invoke();
        }

        public void ResetTimer()
        {
            timer = 0f;
            time = 0f;

            callbackCache = null;
        }

        public void SetTimer(float value, Action callback = null)
        {
            time = value;
            timer = time;

            callbackCache = callback;
        }

        public void AddTimer(float value)
        {
            time += value;
            timer += value;
        }
    }
}
