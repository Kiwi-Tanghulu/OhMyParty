using DG.Tweening;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerVisual : MonoBehaviour
    {
        [Range(0, 255)]
        [SerializeField] private int invisibilityAlpha;
        private Transform playerVisualTrm;
        [SerializeField] private Material invisibilityMat;
        [SerializeField] private ParticleSystem invisiblityParticle;
        private Renderer skinRenderer;
        private Material playerVisualMat;

        private void Start()
        {
            playerVisualTrm = transform.Find("Skin").GetComponent<Transform>();
            skinRenderer = playerVisualTrm.GetComponent<Renderer>();
            playerVisualMat = skinRenderer.material;
        }

        public void ChangeColorInvisibility()
        {
            skinRenderer.material = invisibilityMat;
            invisiblityParticle.Play();
        }

        public void ChangeColorDefault()
        {
            skinRenderer.material = playerVisualMat;
            invisiblityParticle.Stop();
        }
    }
}
