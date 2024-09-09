using DG.Tweening;
using OMG.Minigames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static OMG.UI.GameCycleText;

namespace OMG.UI
{
    public class GameText : MonoBehaviour
    {
        [SerializeField] private float startReadyTextSize;
        [SerializeField] private float endReadyTextSize;

        [Space]
        [SerializeField] private float showTime;
        [SerializeField] private float hideDelayTime;
        [SerializeField] private float hideTime;

        private TextMeshProUGUI text;

        private Sequence seq;

        [Space]
        public UnityEvent OnPlayEvent;
        public UnityEvent OnShowEvent;
        public UnityEvent OnEndEvent;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>(); 

            seq = DOTween.Sequence();
            seq.SetAutoKill(false);

            seq.AppendCallback(() => text.gameObject.SetActive(true));
            seq.AppendCallback(() => OnPlayEvent?.Invoke());
            seq.Append(text.transform.DOScale(Vector3.one * endReadyTextSize, showTime)
                .From(Vector3.one * startReadyTextSize));
            seq.Join(text.DOFade(1f, showTime)
                .From(0f));
            seq.AppendCallback(() => OnShowEvent?.Invoke());
            seq.AppendInterval(hideDelayTime);
            seq.Append(text.DOFade(0f, hideTime));
            seq.AppendCallback(() => OnEndEvent?.Invoke());
            seq.AppendCallback(() => text.gameObject.SetActive(false));

            text.gameObject.SetActive(false);
        }

        public void SetText(string content)
        {
            text.text = content;
        }

        public void Play()
        {
            seq.Restart();
        }
    }
}