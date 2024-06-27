using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        private Vector2 currentTextOriginPos;

        private void Awake()
        {
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
            value = Mathf.Clamp(value, minCount, maxCount);
            currentCountText.text = $"{currentCount}/";
            currentCount = value;
            nextCountText.text = $"{currentCount}/";

            currentCountTextContainer.DOKill();

            currentCountText.rectTransform.anchoredPosition = currentTextOriginPos;
            nextCountText.rectTransform.anchoredPosition = currentTextOriginPos + Vector2.up * -moveAmount;
            currentCountTextContainer.anchoredPosition = Vector2.zero;

            currentCountTextContainer.DOAnchorPosY(moveAmount, textChangeTime).SetEase(Ease.Linear);
        }
    }
}