using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace OMG.UI
{
    public class CountDownUI : MonoBehaviour
    {
        [SerializeField] private Vector2 startCountPos;
        [SerializeField] private Vector2 endCountPos;
        [SerializeField] private float showTextTime;

        [SerializeField] private string finishCountText;
        [SerializeField] private float startFinishSeqTextSize;
        [SerializeField] private float endFinishSeqTextSize;

        private TextMeshProUGUI timeText;

        private Sequence countSeq;
        private Sequence finishSeq;

        private WaitForSeconds showTextWfs;
        private WaitForSeconds finishWfs;
        private Coroutine countDownCoroutine;

        public UnityEvent OnFinishedCountDown;

        private void Awake()
        {
            timeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();

            countSeq = DOTween.Sequence()
                .SetAutoKill(false);
            countSeq.Append(timeText.rectTransform.DOAnchorPos(startCountPos, 0f));
            countSeq.Join(timeText.DOFade(0f, 0f));
            countSeq.Append(timeText.rectTransform.DOAnchorPos(endCountPos, showTextTime));
            countSeq.Join(timeText.DOFade(1f, showTextTime));

            finishSeq = DOTween.Sequence()
                .SetAutoKill(false)
                .OnComplete(() => OnFinishedCountDown?.Invoke());
            finishSeq.Append(timeText.transform.DOScale(Vector3.one * startFinishSeqTextSize, 0f));
            finishSeq.Join(timeText.rectTransform.DOAnchorPos(endCountPos, 0f));
            finishSeq.Join(timeText.DOFade(0f, 0f));
            finishSeq.Append(timeText.transform.DOScale(Vector3.one * endFinishSeqTextSize, showTextTime));
            finishSeq.Join(timeText.DOFade(1f, showTextTime));
            finishSeq.AppendInterval(showTextTime);
            finishSeq.Append(timeText.DOFade(0f, showTextTime));

            showTextWfs = new WaitForSeconds(1f + showTextTime);
            finishWfs = new WaitForSeconds(finishSeq.Duration());

            timeText.gameObject.SetActive(false);
        }

        public void StartCoundDown()
        {
            if (countDownCoroutine != null)
                StopCoroutine(countDownCoroutine);
            countDownCoroutine = StartCoroutine(CoundDownCo());
        }

        private IEnumerator CoundDownCo()
        {
            timeText.gameObject.SetActive(true);

            for (int i = 3; i > 0; i--)
            {
                timeText.text = i.ToString();
                countSeq.Restart();


                yield return showTextWfs;
            }

            timeText.text = finishCountText;
            finishSeq.Restart();

            yield return finishWfs;

            timeText.gameObject.SetActive(false);
        }
    }
}