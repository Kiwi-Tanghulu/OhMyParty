using DG.Tweening;
using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class GameCycleText : MonoBehaviour
    {
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

        private Sequence readyGoSeq;
        private Sequence finishSeq;

        public UnityEvent OnDisplayReady;
        public UnityEvent OnDisplayGo;
        public UnityEvent OnDisplayFinish;
        public UnityEvent OnFinish;

        private void Start()
        {
            Minigame minigame = MinigameManager.Instance.CurrentMinigame;
            minigame.OnFinishGame += Minigame_OnFinishGame;

            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();

            #region readyGo
            readyGoSeq = DOTween.Sequence()
                .SetAutoKill(false);

            //ready 
            readyGoSeq.AppendCallback(() =>
            {
                text.SetText(readyText);
                text.gameObject.SetActive(true);
            });
            readyGoSeq.Append(text.transform.DOScale(Vector3.one * endReadyTextSize, textShowTime)
                .From(Vector3.one * startReadyTextSize));
            readyGoSeq.Join(text.DOFade(1f, textShowTime)
                .From(0f));
            readyGoSeq.AppendCallback(() => OnDisplayReady?.Invoke());

            readyGoSeq.AppendInterval(readyTime);

            //go
            readyGoSeq.AppendCallback(() => text.SetText(goText));
            readyGoSeq.Append(text.transform.DOScale(Vector3.one * endGoTextSize, textShowTime)
                .From(Vector3.one * startGoTextSize));
            readyGoSeq.Join(text.DOFade(1f, textShowTime)
                .From(0f));
            readyGoSeq.AppendCallback(() => OnDisplayGo?.Invoke());
            readyGoSeq.Append(text.DOFade(0f, textShowTime));
            readyGoSeq.AppendCallback(() => text.gameObject.SetActive(false));
            #endregion

            #region finish
            finishSeq = DOTween.Sequence()
                .SetAutoKill(false);

            finishSeq.AppendCallback(() => {
                text.SetText(finishText);
                text.gameObject.SetActive(true);
            });
            finishSeq.Append(text.transform.DOScale(Vector3.one * endFinishTextSize, textShowTime)
                .From(Vector3.one * startFinishTextSize));
            finishSeq.Join(text.DOFade(1f, textShowTime)
                .From(0f));
            finishSeq.AppendCallback(() => OnDisplayFinish?.Invoke());
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
            readyGoSeq.Restart();
        }

        public void PlayFinish()
        {
            finishSeq.Restart();
        }
    }
}