using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro fullScoreText;
        [SerializeField] private TextMeshPro additiveScoreText;
        [SerializeField] private int textChangeTime;
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
            float percent = 0f;
            float score = 0f;

            while(percent < 1f)
            {
                percent += Time.deltaTime / textChangeTime;

                score = Mathf.Lerp(prevScore, currentScore, percent);

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
