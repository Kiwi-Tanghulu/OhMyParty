using OMG.Lobbies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class CameraRenderPlayerVisual : PlayerVisual
    {
        private ulong ownerID;

        private Animator anim;
        private int isReadyHash = Animator.StringToHash("isReady");

        protected override void Start()
        {
            base.Start();

            anim = GetComponent<Animator>();

            LobbyReadyComponent lobbyReady = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        public void SetOwenrID(ulong ownerID)
        {
            this.ownerID = ownerID;
        }

        private void MinigameInfoUI_OnPlayerReadyEvent(ulong clientID)
        {
            if (Lobby.Current.LobbyState == LobbyState.MinigameSelected)
            {
                if (clientID == ownerID)
                {
                    anim.SetBool(isReadyHash, true);
                }
            }
        }
    }
}