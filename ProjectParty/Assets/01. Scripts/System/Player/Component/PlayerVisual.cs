using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        private PlayerVisualInfo visualInfo;
        public PlayerVisualInfo VisualInfo => visualInfo;

        private SkinnedMeshRenderer render;

        private void Awake()
        {
            render = transform.Find("Skin").GetComponent<SkinnedMeshRenderer>();
        }

        public void SetVisual(PlayerVisualInfo newVisualInfo)
        {
            visualInfo = newVisualInfo;

            render.sharedMesh = visualInfo.mesh;
        }
    }
}