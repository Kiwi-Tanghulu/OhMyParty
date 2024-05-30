using OMG.Lobbies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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

            Lobby.Current.OnLobbyStateChangedEvent += Lobby_OnLobbyStateChangedEvent;
            LobbyReadyComponent lobbyReady = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        

        public void SetOwenrID(ulong ownerID)
        {
            this.ownerID = ownerID;
        }

        public void SetReady(bool value)
        {
            anim.SetBool(isReadyHash, true);
        }

        private void Lobby_OnLobbyStateChangedEvent(LobbyState state)
        {
            //bagguayaham
            if(state == LobbyState.MinigameFinished)
                SetReady(false);

        }

        private void MinigameInfoUI_OnPlayerReadyEvent(ulong clientID)
        {
            if (Lobby.Current.LobbyState == LobbyState.MinigameSelected)
                SetReady(clientID == ownerID);
        }
    }
}