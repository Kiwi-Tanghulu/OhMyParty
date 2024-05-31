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

        private PlayerRagdollController ragdoll;
        public PlayerRagdollController Ragdoll => ragdoll;

        private SkinnedMeshRenderer skin;

        private void Awake()
        {
            skin = transform.Find("Skin").GetComponent<SkinnedMeshRenderer>();
            ragdoll = GetComponent<PlayerRagdollController>();
        }

        protected virtual void Start()
        {
            SetVisual(visualType);//test
        }

        public void SetVisual(PlayerVisualType newVisualType)
        {
            visualType = newVisualType;

            skin.sharedMesh = PlayerManager.Instance.PlayerVisualList[visualType].VisualMesh;
        }
    }
}