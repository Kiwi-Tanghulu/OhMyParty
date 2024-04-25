using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        private PlayerVisualType visualType;
        public PlayerVisualType VisualType => visualType;

        [SerializeField] private SkinnedMeshRenderer render;

        private void Awake()
        {
            if (render == null)
                render = transform.Find("Skin").GetComponent<SkinnedMeshRenderer>();
        }

        private void Start()
        {
            SetVisual(PlayerVisualType.BoxHead);//test
        }

        public void SetVisual(PlayerVisualType newVisualType)
        {
            visualType = newVisualType;

            render.sharedMesh = PlayerManager.Instance.playerVisualInfo[visualType];
        }
    }
}