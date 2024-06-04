using DG.Tweening;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerVisual : MonoBehaviour
    {
        [Range(0, 255)]
        [SerializeField] private int invisibilityAlpha;
        [SerializeField] private Transform playerVisualTrm;
        [SerializeField] private Material invisibilityMat;
        [SerializeField] private ParticleSystem invisiblityParticle;
        private Renderer skinRenderer;
        private Material playerVisualMat;
        private Color32 visualDefaultColor;
        
        //private Sequence effectSequence;
        //private float h, s, v; // HSV 값을 위한 변수
        //private float initialV; // 초기 V 값 저장용

        private void Awake()
        {
            skinRenderer = playerVisualTrm.GetComponent<Renderer>();
            playerVisualMat = skinRenderer.material;
            visualDefaultColor = playerVisualMat.color;
            //Color.RGBToHSV(visualDefaultColor, out h, out s, out v);
            //initialV = v;
            //effectSequence = DOTween.Sequence();
        }

        public void ChangeColorInvisibility()
        {
            //playerVisualMat.color = new Color32(visualDefaultColor.r, visualDefaultColor.g, visualDefaultColor.b, (byte)invisibilityAlpha);
            Debug.Log("플레이어 비쭈얼");
            skinRenderer.material = invisibilityMat;
            invisiblityParticle.Play();
        }

        public void ChangeColorDefault()
        {
            //playerVisualMat.color = visualDefaultColor;
            skinRenderer.material = playerVisualMat;
            invisiblityParticle.Stop();
        }

    //    private void BlinkVisual()
    //    {
    //        playerVisualMat.EnableKeyword("_EMISSION");

    //        effectSequence = DOTween.Sequence();
    //        effectSequence.Append(DOTween.To(() => v, x => SetEmissionV(x), 1.0f, 0.5f)
    //                       .SetEase(Ease.InOutSine))
    //                      .Append(DOTween.To(() => v, x => SetEmissionV(x), 0.0f, 0.5f)
    //                       .SetEase(Ease.InOutSine))
    //                      .SetLoops(-1, LoopType.Yoyo);
    //    }

    //    private void SetEmissionV(float value)
    //    {
    //        v = value;
    //        Color finalColor = Color.HSVToRGB(h, s, v);
    //        playerVisualMat.SetColor("_EMISSION", finalColor * Mathf.LinearToGammaSpace(1.0f));
    //    }
    }
}
