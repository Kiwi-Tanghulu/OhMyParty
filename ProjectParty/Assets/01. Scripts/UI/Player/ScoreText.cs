using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro fullScoreText;
        [SerializeField] private TextMeshPro additiveScoreText;
        [SerializeField] private int textChangeSpeed;
        [SerializeField] private float hideDelay;

        public UnityEvent OnStartCalcEvent;
        public UnityEvent OnStopCalcEvent;

        private WaitForSeconds wfs;

        private Animator anim;

        private int prevScore;
        private int currentScore;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            wfs = new WaitForSeconds(hideDelay);

            Hide();
        }

        public void SetScore(int newScore)
        {
            if (newScore < 0)
                return;

            prevScore = currentScore;
            currentScore = newScore;
        }

        public void Show()
        {
            gameObject.SetActive(true);

            fullScoreText.text = prevScore.ToString();
            additiveScoreText.text = $"+{(currentScore - prevScore)}";

            anim.Play("Show", 0, 0f);

            OnStartCalcEvent?.Invoke();

            StartCoroutine(TextChangeCo());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator TextChangeCo()
        {
            float score = prevScore;

            while (score < currentScore)
            {
                score += Time.deltaTime * textChangeSpeed;
                score = Mathf.Min(score, currentScore);

                fullScoreText.text = $"{(int)score}";

                yield return null;
            }

            fullScoreText.text = $"{(int)currentScore}";

            OnStopCalcEvent?.Invoke();

            yield return wfs;

            Hide();
        }
    }
}
