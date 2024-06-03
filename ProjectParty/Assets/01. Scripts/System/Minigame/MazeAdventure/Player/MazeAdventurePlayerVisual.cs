using DG.Tweening;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerVisual : MonoBehaviour
    {
        [Range(0, 255)]
        [SerializeField] private int invisibilityAlpha;
        [SerializeField] private Transform playerVisualTrm;

        private Material playerVisualMat;
        private Color32 visualDefaultColor;
        private Sequence effectSequence;
        private float h, s, v; // HSV 값을 위한 변수
        private float initialV; // 초기 V 값 저장용

        private void Awake()
        {
            playerVisualMat = playerVisualTrm.GetComponent<Renderer>().material;
            visualDefaultColor = playerVisualMat.color;
            Color.RGBToHSV(visualDefaultColor, out h, out s, out v);
            initialV = v;
            effectSequence = DOTween.Sequence();
        }

        public void ChangeColorInvisibility()
        {
            playerVisualMat.color = new Color32(visualDefaultColor.r, visualDefaultColor.g, visualDefaultColor.b, (byte)invisibilityAlpha);
            BlinkVisual();
        }

        public void ChangeColorDefault()
        {
            playerVisualMat.color = visualDefaultColor;
            playerVisualMat.DisableKeyword("_EMISSION"); // Emission 비활성화
            DOTween.Kill(playerVisualMat); // 기존 Tween 효과 중지
        }

        private void BlinkVisual()
        {
            playerVisualMat.EnableKeyword("_EMISSION");

            // V 값을 0과 1 사이에서 깜빡이게 하는 Tween
            effectSequence = DOTween.Sequence();
            effectSequence.Append(DOTween.To(() => v, x => SetEmissionV(x), 1.0f, 0.5f)
                           .SetEase(Ease.InOutSine)) // 부드러운 깜빡임을 위한 Ease
                          .Append(DOTween.To(() => v, x => SetEmissionV(x), 0.0f, 0.5f)
                           .SetEase(Ease.InOutSine)) // 부드러운 깜빡임을 위한 Ease
                          .SetLoops(-1, LoopType.Yoyo); // 무한 반복
        }

        private void SetEmissionV(float value)
        {
            v = value;
            Color finalColor = Color.HSVToRGB(h, s, v);
            playerVisualMat.SetColor("_EmissionColor", finalColor * Mathf.LinearToGammaSpace(1.0f)); // Emission Intensity는 1로 고정
        }
    }
}
