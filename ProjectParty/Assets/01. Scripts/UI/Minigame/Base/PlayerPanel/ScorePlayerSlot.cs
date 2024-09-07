using OMG.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI.Minigames
{
    public class ScorePlayerSlot : PlayerSlot
    {
        [SerializeField] TMP_Text scoreText = null;
        [SerializeField] bool stringFormat = false;
        [ConditionalField("stringFormat", true)]
        [SerializeField] string format = "#";

        [Space(15f)]
        [SerializeField] UnityEvent<int> OnScoreChangedEvent = null;

        private int score = 0;
        public int Score => score;

        protected override void Awake()
        {
            base.Awake();
            Reset();
        }

        public void Reset()
        {
            scoreText.text = "-";
            score = 0;
        }

        public void SetScore(int newScore)
        {
            int prevScore = score;
            score = newScore;

            string text = score.ToString(stringFormat ? format : "");
            scoreText.text = text;

            if(prevScore != score)
                OnScoreChangedEvent?.Invoke(score - prevScore);
        }
    }
}
