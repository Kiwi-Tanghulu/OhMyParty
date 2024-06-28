using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace OMG.UI
{
    public class CountText : UIObject
    {
        [SerializeField] private TextMeshProUGUI baseText;
        [SerializeField] private TextMeshProUGUI currentCountText;
        [SerializeField] private TextMeshProUGUI nextCountText;
        [SerializeField] private RectTransform currentCountTextContainer;

        [Space]
        [SerializeField] private float textChangeTime;
        [SerializeField] private float moveAmount = -400f;

        private int minCount;
        private int maxCount;
        private int currentCount = 0;
        private int prevCount = 0;

        private Vector2 currentTextOriginPos;

        public override void Init()
        {
            base.Init();

            currentTextOriginPos = currentCountText.rectTransform.anchoredPosition;
        }

        public void SetCountValue(int minCount, int maxCount)
        {
            this.minCount = minCount;
            this.maxCount = maxCount;

            baseText.text = $"/{maxCount}";
            currentCountText.text = $"{minCount}/";

            currentCountText.rectTransform.anchoredPosition = currentTextOriginPos;
            nextCountText.rectTransform.anchoredPosition = currentTextOriginPos + Vector2.up * -moveAmount;
            currentCountTextContainer.anchoredPosition = Vector2.zero;
        }

        public void SetCount(int value)
        {
            prevCount = currentCount;
            value = Mathf.Clamp(value, minCount, maxCount);
            currentCount = value;
        }

        public void PlayAnim(float delay, Action onStart, Action onEnd)
        {
            currentCountText.text = $"{prevCount}/";
            nextCountText.text = $"{currentCount}/";

            currentCountTextContainer.DOKill();

            currentCountText.rectTransform.anchoredPosition = currentTextOriginPos;
            nextCountText.rectTransform.anchoredPosition = currentTextOriginPos + Vector2.up * -moveAmount;
            currentCountTextContainer.anchoredPosition = Vector2.zero;

            currentCountTextContainer.DOAnchorPosY(moveAmount, textChangeTime).SetEase(Ease.Linear)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onEnd?.Invoke())
                .SetDelay(delay);
        }
    }
}