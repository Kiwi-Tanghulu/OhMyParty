using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class ReadyGoUI : MonoBehaviour
    {
        [SerializeField] private string readyText;
        [SerializeField] private string goText;

        [SerializeField] private float startReadyTextSize;
        [SerializeField] private float endReadyTextSize;
        [SerializeField] private float startGoTextSize;
        [SerializeField] private float endGoTextSize;
        [SerializeField] private float textShowTime;
        [SerializeField] private float readyTime;

        private TextMeshProUGUI text;

        private Sequence readyGoSeq;

        public UnityEvent OnFinishReadyGo;

        private void Awake()
        {
            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();

            //create seq
            readyGoSeq = DOTween.Sequence()
                .SetAutoKill(false)
                .OnComplete(() => OnFinishReadyGo?.Invoke());

            //ready 
            readyGoSeq.AppendCallback(() => text.SetText("Ready"));
            readyGoSeq.Append(text.transform.DOScale(Vector3.one * endReadyTextSize, textShowTime)
                .From(Vector3.one * startReadyTextSize));
            readyGoSeq.Join(text.DOFade(1f, textShowTime)
                .From(0f));

            readyGoSeq.AppendInterval(readyTime);

            //go
            readyGoSeq.AppendCallback(() => text.SetText("Go"));
            readyGoSeq.Append(text.transform.DOScale(Vector3.one * endGoTextSize, textShowTime)
                .From(Vector3.one * startGoTextSize));
            readyGoSeq.Join(text.DOFade(1f, textShowTime)
                .From(0f));
            readyGoSeq.Append(text.DOFade(0f, textShowTime));

            text.gameObject.SetActive(false);
        }

        public void Play()
        {
            readyGoSeq.Restart();
        }
    }
}