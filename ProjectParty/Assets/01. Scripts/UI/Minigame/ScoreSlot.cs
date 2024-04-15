using TinyGiantStudio.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI.Minigames
{
    public class ScoreSlot : MonoBehaviour
    {
        [SerializeField] Modular3DText scoreText = null;
        [SerializeField] MeshRenderer profileRenderer = null;

        [Space(15f)]
        [SerializeField] UnityEvent<int> OnScoreChangedEvent = null;

        private Material profileMaterial = null;
        private int score = 0;

        private void Awake()
        {
            profileMaterial = profileRenderer.material;
        }

        public void Init()
        {
            scoreText.Text = "-";
            score = 0;
        }

        public void SetProfile(Sprite profileImage)
        {
            profileMaterial.SetTexture("_MainTex", profileImage.texture);
        }

        public void SetScore(int newScore)
        {
            int prevScore = score;
            score = newScore;
            scoreText.Text = score.ToString("00");

            if(prevScore != score)
                OnScoreChangedEvent?.Invoke(score - prevScore);
        }
    }
}