using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Ragdoll;
using OMG.Skins;
using JetBrains.Annotations;

namespace OMG.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private PlayerVisualType visualType;
        public PlayerVisualType VisualType => visualType;

        private PlayerRagdollController ragdoll;
        public PlayerRagdollController Ragdoll => ragdoll;

        //private SkinnedMeshRenderer skin;

        //private void Awake()
        //{
        //    skin = transform.Find("Skin").GetComponent<SkinnedMeshRenderer>();
        //    ragdoll = GetComponent<PlayerRagdollController>();
        //}

        protected virtual void Start()
        {
            SetSkin(visualType);
        }

        //public void SetVisual(PlayerVisualType newVisualType)
        //{
        //    visualType = newVisualType;

        //    skin.sharedMesh = PlayerManager.Instance.PlayerVisualList[visualType].VisualMesh;
        //}

        private SkinSelector skinSelector;

        private void Awake()
        {
            skinSelector = GetComponent<SkinSelector>();
        }

        public void SetSkin(PlayerVisualType type)
        {
            //skinSelector.SetSkin((int)type);
        }
    }
}