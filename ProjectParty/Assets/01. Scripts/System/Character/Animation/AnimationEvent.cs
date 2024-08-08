using System;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    public class AnimationEvent : MonoBehaviour
    {
        public UnityEvent OnStartEvent;
        public UnityEvent OnPlayingEvent;
        public UnityEvent OnPlayingSubEvent;
        public UnityEvent OnEndEvent;

        public void InvokeStartEvent() => OnStartEvent?.Invoke();
        public void InvokePlayingEvent() => OnPlayingEvent?.Invoke();
        public void InvokePlayingSubEvent() => OnPlayingSubEvent?.Invoke();
        public void InvokeEndEvent() => OnEndEvent?.Invoke();
    }
}