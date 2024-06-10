using OMG.Lobbies;
using OMG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace OMG.Player
{
    public class LobbyPlayerController : PlayerController
    {
        private RenderTargetPlayerVisual renderTargetPlayerVisual;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            renderTargetPlayerVisual = PlayerManager.Instance.CreatePlayerRenderTarget(this);
            
            Lobby lobby = Lobby.Current;
            lobby.PlayerContainer.RegistPlayer(this);
            lobby.OnLobbyStateChangedEvent += Lobby_OnLobbyStateChangedEvent;
            LobbyReadyComponent lobbyReady = lobby.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            Lobby.Current.PlayerContainer.UnregistPlayer(this);
        }

        private void Lobby_OnLobbyStateChangedEvent(LobbyState state)
        {
            switch(state)
            {
                case LobbyState.MinigameFinished:
                    renderTargetPlayerVisual.SetPose(RenderTargetPlayerPoseType.Idle);
                    StateMachine.ChangeState(typeof(CalculateScoreState));
                    break;
            }
        }

        private void MinigameInfoUI_OnPlayerReadyEvent(ulong clientID)
        {
            if (Lobby.Current.LobbyState == LobbyState.MinigameSelected)
            {
                if (clientID == OwnerClientId)
                {
                    renderTargetPlayerVisual.SetPose(RenderTargetPlayerPoseType.Ready);
                }
            }
        }
    }
}
