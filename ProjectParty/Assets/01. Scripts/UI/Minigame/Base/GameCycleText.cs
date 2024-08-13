using DG.Tweening;
using OMG.Minigames;
using OMG.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class GameCycleText : MonoBehaviour
    {
        public class ReadyGoText
        {
            private Sequence seq;

            private Action readyPositiveCallback = null;
            private Action readyNegativeCallback = null;
            private Action goPositiveCallback = null;
            private Action goNegativeCallback = null;

            public ReadyGoText(TextMeshProUGUI text, string readyText, string goText, float readyTime,
                Func<TextMeshProUGUI, Tween> createReadyTween, Func<TextMeshProUGUI, Tween> createGoTween)
            {
                seq = DOTween.Sequence()
                    .SetAutoKill(false);

                //ready 
                seq.AppendCallback(() =>
                {
                    text.SetText(readyText);
                    text.gameObject.SetActive(true);
                    readyPositiveCallback?.Invoke();
                });
                seq.Append(createReadyTween?.Invoke(text).SetAutoKill(false));
                seq.AppendCallback(() => readyNegativeCallback?.Invoke());

                seq.AppendInterval(readyTime);

                //go
                seq.AppendCallback(() =>
                {
                    text.SetText(goText);
                    goPositiveCallback?.Invoke();
                });
                seq.Append(createGoTween?.Invoke(text).SetAutoKill(false));
                seq.AppendCallback(() => goNegativeCallback?.Invoke());
                seq.AppendCallback(() => text.gameObject.SetActive(false));
            }

            public void Play()
            {
                seq.Restart();
            }

            public ReadyGoText AddReadyPositiveCallback(Action callback)
            {
                readyPositiveCallback += callback;
                return this;
            }

            public ReadyGoText AddReadyNegativeCallback(Action callback)
            {
                readyNegativeCallback += callback;
                return this;
            }

            public ReadyGoText AddGoPositiveCallback(Action callback)
            {
                goPositiveCallback += callback;
                return this;
            }

            public ReadyGoText AddGoNegativeCallback(Action callback)
            {
                goNegativeCallback += callback;
                return this;
            }
        }

        [SerializeField] private string readyText;
        [SerializeField] private string goText;
        [SerializeField] private string finishText;

        [Space]
        [SerializeField] private float startReadyTextSize;
        [SerializeField] private float endReadyTextSize;
        [SerializeField] private float startGoTextSize;
        [SerializeField] private float endGoTextSize;

        [Space]
        [SerializeField] private float startFinishTextSize;
        [SerializeField] private float endFinishTextSize;
        //[SerializeField] private float finishDelayTime;

        [Space]
        [SerializeField] private float textShowTime;
        [SerializeField] private float readyTime;

        private TextMeshProUGUI text;

        private Sequence finishSeq;

        private ReadyGoText readyGo;
        public ReadyGoText ReadyGo => readyGo;

        public OptOption<UnityEvent> FinishEventOption;

        public UnityEvent OnFinish;

        private void Start()
        {
            Minigame minigame = MinigameManager.Instance.CurrentMinigame;
            minigame.OnFinishEvent.AddListener(Minigame_OnFinishGame);

            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();

            readyGo = new ReadyGoText(text, readyText, goText, readyTime, (t) =>
            {
                Sequence seq = DOTween.Sequence();

                seq.Append(text.transform.DOScale(Vector3.one * endReadyTextSize, textShowTime)
                .From(Vector3.one * startReadyTextSize));
                seq.Join(text.DOFade(1f, textShowTime)
                    .From(0f));

                return seq;
            }, (t) =>
            {
                Sequence seq = DOTween.Sequence();

                seq.Append(text.transform.DOScale(Vector3.one * endGoTextSize, textShowTime)
                .From(Vector3.one * startGoTextSize));
                seq.Join(text.DOFade(1f, textShowTime)
                    .From(0f));

                return seq;
            });

            #region finish
            finishSeq = DOTween.Sequence()
                .SetAutoKill(false);

            finishSeq.AppendCallback(() => {
                text.SetText(finishText);
                text.gameObject.SetActive(true);
                FinishEventOption[true]?.Invoke();
            });
            finishSeq.Append(text.transform.DOScale(Vector3.one * endFinishTextSize, textShowTime)
                .From(Vector3.one * startFinishTextSize));
            finishSeq.Join(text.DOFade(1f, textShowTime)
                .From(0f));
            finishSeq.AppendCallback(() => FinishEventOption[false]?.Invoke());
            finishSeq.AppendInterval(minigame.MinigameData.OutroPostponeTime);
            finishSeq.AppendCallback(() => OnFinish?.Invoke());
            finishSeq.AppendCallback(() => text.gameObject.SetActive(false));
            #endregion

            text.gameObject.SetActive(false);
        }

        private void Minigame_OnFinishGame()
        {
            PlayFinish();
        }

        public void PlayRaedyGo()
        {
            readyGo.Play();
        }

        public void PlayFinish()
        {
            finishSeq.Restart();
        }
    }
}