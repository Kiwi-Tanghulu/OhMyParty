using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Ragdoll;

namespace OMG.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private PlayerVisualType visualType;
        public PlayerVisualType VisualType => visualType;

        [SerializeField] private SkinnedMeshRenderer skin;

        [Space]
        [SerializeField] private RagdollController ragdoll;
        public RagdollController Ragdoll => ragdoll;

        private void Awake()
        {
            if (skin == null)
                skin = transform.Find("Skin").GetComponent<SkinnedMeshRenderer>();
        }

        private void Start()
        {
            SetVisual(visualType);//test
        }

        public void SetVisual(PlayerVisualType newVisualType)
        {
            visualType = newVisualType;

            skin.sharedMesh = PlayerManager.Instance.playerVisualInfo[visualType];
        }
    }
}