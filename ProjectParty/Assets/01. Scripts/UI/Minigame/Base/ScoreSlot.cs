using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI.Minigames
{
    public class ScoreSlot : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText = null;

        [Space(15f)]
        [SerializeField] UnityEvent<int> OnScoreChangedEvent = null;

        private int score = 0;

        public void Init()
        {
            scoreText.text = "-";
            score = 0;
        }

        public void SetScore(int newScore)
        {
            int prevScore = score;
            score = newScore;
            scoreText.text = score.ToString("00");

            if(prevScore != score)
                OnScoreChangedEvent?.Invoke(score - prevScore);
        }
    }
}