using System;
using UnityEngine;

namespace OMG
{
    public class AnimationEvent : MonoBehaviour
    {
        public event Action OnStartEvent;
        public event Action OnPlayingEvent;
        public event Action OnEndEvent;

        public void InvokeStartEvent() => OnStartEvent?.Invoke();
        public void InvokePlayingEvent() => OnPlayingEvent?.Invoke();
        public void InvokeEndEvent() => OnEndEvent?.Invoke();
    }
}