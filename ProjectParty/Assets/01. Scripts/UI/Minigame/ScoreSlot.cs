using TinyGiantStudio.Text;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class ScoreSlot : MonoBehaviour
    {
        [SerializeField] Modular3DText scoreText = null;
        [SerializeField] MeshRenderer profileRenderer = null;

        private Material profileMaterial = null;

        private void Awake()
        {
            profileMaterial = profileRenderer.material;
        }

        public void Init()
        {
            scoreText.Text = "-";
        }

        public void SetProfile(Sprite profileImage)
        {
            profileMaterial.SetTexture("_MainTex", profileImage.texture);
        }

        public void SetScore(int score)
        {
            scoreText.Text = score.ToString("00");
        }
    }
}