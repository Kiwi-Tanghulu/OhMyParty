using OMG.Skins;
using System;
using Unity.Netcode;
using OMG.Lobbies;
using OMG.Extensions;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerVisual : NetworkBehaviour
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

        private bool visualSetted;
        private ulong ownerID = 0;

        protected virtual void Awake()
        {
            skinSelector = GetComponent<PlayerSkinSelector>();
            ragdoll = GetComponent<PlayerRagdollController>();
        }

        public void Init(ulong ownerID)
        {
            this.ownerID = ownerID;

            TrySetVisual();
            Lobby.Current.PlayerDatas.OnListChanged += HandlePlayerDatasChanged;
        }

        private void TrySetVisual()
        {
            int index = Lobby.Current.PlayerDatas.Find(out PlayerData data, data => data.ClientID == ownerID);
            if (index == -1)
                return;

            visualSetted = true;
            SetSkin((PlayerVisualType)data.VisualType);
        }

        private void HandlePlayerDatasChanged(NetworkListEvent<PlayerData> listEvent)
        {
            TrySetVisual();

            if (visualSetted)
                Lobby.Current.PlayerDatas.OnListChanged -= HandlePlayerDatasChanged;
        }

        public void SetSkin(PlayerVisualType type)
        {
            skinSelector.SetSkin(skinLibrarySO[(int)type]);
            skinSelector.CurrentSkin.transform.localPosition = skinPos;

            OnSkinChangedEvent?.Invoke(type);
        }
    }
}