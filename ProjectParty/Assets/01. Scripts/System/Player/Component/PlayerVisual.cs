using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Ragdoll;
using OMG.Skins;
using JetBrains.Annotations;
using System;

namespace OMG.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private SkinLibrarySO skinLibrarySO;
        [SerializeField] private PlayerVisualType visualType;
        public PlayerVisualType VisualType => visualType;
        [SerializeField] private Vector3 skinPos;
        private PlayerSkinSelector skinSelector;
        public PlayerSkinSelector SkinSelector => skinSelector;

        private PlayerRagdollController ragdoll;
        public PlayerRagdollController Ragdoll => ragdoll;

        public Action<PlayerVisualType/*new skin type*/> OnSkinChangedEvent;

        protected virtual void Awake()
        {
            skinSelector = GetComponent<PlayerSkinSelector>();
            ragdoll = GetComponent<PlayerRagdollController>();

            SetSkin(visualType);
        }

        public void SetSkin(PlayerVisualType type)
        {
            skinSelector.SetSkin(skinLibrarySO[(int)type]);
            skinSelector.CurrentSkin.transform.localPosition = skinPos;

            OnSkinChangedEvent?.Invoke(type);
        }
    }
}