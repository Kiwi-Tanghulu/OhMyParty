using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace OMG.Tweens
{
    public abstract class TweenSO : ScriptableObject
    {
        [SerializeField] protected List<TweenParam> tweenParams = new List<TweenParam>();

        private Sequence sequence = null;
        protected Transform body = null;

        public event Action OnTweenCompletedEvent = null;
        public bool IsTweening => sequence.IsActive();

        public virtual TweenSO CreateInstance(Transform body)
        {
            TweenSO instance = ScriptableObject.Instantiate(this);
            instance.body = body;

            return instance;
        }

        public void PlayTween(Action callback = null)
        {
            CreateTween(callback);
            sequence.Play();
        }

        public Tween CreateTween(Action callback = null)
        {
            sequence = DOTween.Sequence();
            sequence.OnComplete(() =>
            {
                HandleTweenCompleted();
                callback?.Invoke();
            });

            OnTween(sequence);
            return sequence;
        }

        public void ForceKillTween()
        {
            sequence?.Kill();
        }

        public TweenParam GetParam(int index) => tweenParams[index];

        protected virtual void HandleTweenCompleted()
        {
            OnTweenCompletedEvent?.Invoke();
            sequence?.Kill();
        }

        protected abstract void OnTween(Sequence sequence);
    }
}