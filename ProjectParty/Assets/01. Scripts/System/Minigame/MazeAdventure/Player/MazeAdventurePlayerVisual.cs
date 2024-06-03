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
        private float h, s, v; // HSV ���� ���� ����
        private float initialV; // �ʱ� V �� �����

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
            playerVisualMat.DisableKeyword("_EMISSION"); // Emission ��Ȱ��ȭ
            DOTween.Kill(playerVisualMat); // ���� Tween ȿ�� ����
        }

        private void BlinkVisual()
        {
            playerVisualMat.EnableKeyword("_EMISSION");

            // V ���� 0�� 1 ���̿��� �����̰� �ϴ� Tween
            effectSequence = DOTween.Sequence();
            effectSequence.Append(DOTween.To(() => v, x => SetEmissionV(x), 1.0f, 0.5f)
                           .SetEase(Ease.InOutSine)) // �ε巯�� �������� ���� Ease
                          .Append(DOTween.To(() => v, x => SetEmissionV(x), 0.0f, 0.5f)
                           .SetEase(Ease.InOutSine)) // �ε巯�� �������� ���� Ease
                          .SetLoops(-1, LoopType.Yoyo); // ���� �ݺ�
        }

        private void SetEmissionV(float value)
        {
            v = value;
            Color finalColor = Color.HSVToRGB(h, s, v);
            playerVisualMat.SetColor("_EmissionColor", finalColor * Mathf.LinearToGammaSpace(1.0f)); // Emission Intensity�� 1�� ����
        }
    }
}
