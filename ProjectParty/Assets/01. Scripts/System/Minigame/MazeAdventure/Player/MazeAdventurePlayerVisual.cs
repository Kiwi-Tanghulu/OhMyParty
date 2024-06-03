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

        private Material playerVisualMat;
        private Color32 visualDefaultColor;

        private void Awake()
        {
            playerVisualMat = playerVisualTrm.GetComponent<Renderer>().material;
            visualDefaultColor = playerVisualMat.color;
        }

        public void ChangeColorInvisibility()
        {
            playerVisualMat.color = new Color32(visualDefaultColor.r, visualDefaultColor.g, visualDefaultColor.b, (byte)invisibilityAlpha);
        }

        public void ChangeColorDefault()
        {
            playerVisualMat.color = visualDefaultColor;
        }

        //private IEnumerator BlinkVisual()
        //{
            
        //}
    }
}
