using OMG.Skins;
using System;
using Unity.Netcode;
using OMG.Lobbies;
using OMG.Extensions;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerVisual : CharacterComponent
    {
        [SerializeField] private SkinLibrarySO skinLibrarySO;
        [SerializeField] private PlayerVisualType visualType;
        public PlayerVisualType VisualType => visualType;
        [SerializeField] private Vector3 skinPos;
        private PlayerSkinSelector skinSelector;
        public PlayerSkinSelector SkinSelector => skinSelector;

        private PlayerRagdollController ragdoll;
        public PlayerRagdollController Ragdoll => ragdoll;

        private ExtendedAnimator anim;
        public ExtendedAnimator Anim => anim;

        public Action<PlayerVisualType/*new skin type*/> OnSkinChangedEvent;

        private bool visualSetted;
        protected ulong ownerID = 0;

        public override void Init(OMG.CharacterController controller)
        {
            skinSelector = GetComponent<PlayerSkinSelector>();
            ragdoll = GetComponent<PlayerRagdollController>();
            anim = GetComponent<ExtendedAnimator>();

            ownerID = controller.OwnerClientId;

            visualSetted = false;
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