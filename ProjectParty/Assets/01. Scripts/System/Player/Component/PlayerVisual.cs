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
        protected ulong ownerID = 0;

        protected virtual void Awake()
        {
            
        }

        public virtual void Init(ulong ownerID)
        {
            this.ownerID = ownerID;
            visualSetted = false;

            skinSelector = GetComponent<PlayerSkinSelector>();
            ragdoll = GetComponent<PlayerRagdollController>();

            if (Lobby.Current != null)
            {
                TrySetVisual();

                if (!visualSetted)
                    Lobby.Current.PlayerDatas.OnListChanged += HandlePlayerDatasChanged;
            }
            else
            {
                SetSkin(visualType);
            }
        }

        private void TrySetVisual()
        {
            int index = Lobby.Current.PlayerDatas.Find(out PlayerData data, data => data.ClientID == ownerID);
            if (index == -1)
                return;

            visualSetted = true;
            SetSkin((PlayerVisualType)data.VisualType);

            skinSelector.CurrentSkin.GetComponent<PlayerOutLine>()?.SettingOutLine(index);
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