using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerVisual : MonoBehaviour
    {
        [Range(0, 255)]
        [SerializeField] private int invisibilityAlpha;

        [SerializeField] private Transform playerVisualTrm;
        [SerializeField] private Transform playerRagdollTrm;

        private Material playerVisualMat;
        private Material playerRagdollMat;

        private Color32 visualDefaultColor;
        private Color32 ragdollDefaultColor;

        private void Awake()
        {
            playerVisualMat = playerVisualTrm.GetComponent<Renderer>().material;
            playerRagdollMat = playerRagdollTrm.GetComponent<Renderer>().material;
            visualDefaultColor = playerVisualMat.color;
            ragdollDefaultColor = playerRagdollMat.color;
        }

        public void ChangeColorInvisibility()
        {
            playerVisualMat.color = new Color32(visualDefaultColor.r, visualDefaultColor.g, visualDefaultColor.b, (byte)invisibilityAlpha);
            playerRagdollMat.color = new Color32(ragdollDefaultColor.r, ragdollDefaultColor.g, ragdollDefaultColor.b, (byte)invisibilityAlpha);
        }

        public void ChangeColorDefault()
        {
            playerVisualMat.color = visualDefaultColor;
            playerRagdollMat.color = ragdollDefaultColor;
        }
    }
}
