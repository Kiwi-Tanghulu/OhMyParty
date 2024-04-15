using System.Text;
using TinyGiantStudio.Text;
using UnityEngine;

namespace OMG.Feedbacks.Minigames
{
    public class ScoreSlotFeedback : Feedback
    {
        [SerializeField] Modular3DText textPrefab = null;
        [SerializeField] Transform feedbackPosition = null;

        private int scoreDiff = 0;

        public void HandleScoreChanged(int scoreDiff)
        {
            this.scoreDiff = scoreDiff;
        }

        public override void Play(Transform playTrm)
        {
            Modular3DText instance = Instantiate(textPrefab, feedbackPosition);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.Text = GetScoreText();
        }

        private string GetScoreText()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Mathf.Sign(scoreDiff) == 1f ? "+" : "-");
            builder.Append(Mathf.Abs(scoreDiff).ToString());
            return builder.ToString();
        }
    }
}
