using System;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Timers
{
    public class Timer : MonoBehaviour
    {
        public enum ValueType
        {
            Ratio,
            Single
        }

        [SerializeField] ValueType valueType = ValueType.Ratio;
        
        public UnityEvent<float> OnValueChangedEvent = new UnityEvent<float>();
        public UnityEvent OnTimerFinishedEvent = new UnityEvent();

        private Action callbackCache = null;

        private float timer = 0f;
        private float time = 0f;

        public float Ratio => 1f - timer / time;
        public bool Finished => timer <= 0f;

        private void Update()
        {
            if(Finished)
                return;

            timer -= Time.deltaTime;
            OnValueChangedEvent?.Invoke((valueType == ValueType.Ratio) ? Ratio : timer);
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
