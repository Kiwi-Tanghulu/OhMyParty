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
        [SerializeField] private ScoreText scoreText;

        private RenderTargetPlayerVisual renderTargetPlayerVisual;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            renderTargetPlayerVisual = PlayerManager.Instance.CreatePlayerRenderTarget(this);
            
            Lobby lobby = Lobby.Current;
            lobby.PlayerContainer.RegistPlayer(this);
            lobby.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] += LobbyCutSscene_OnEndFinish;
            lobby.OnLobbyStateChangedEvent += Lobby_OnLobbyStateChangedEvent;
            LobbyReadyComponent lobbyReady = lobby.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            Lobby.Current.PlayerContainer.UnregistPlayer(this);
            Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] -= LobbyCutSscene_OnEndFinish;
        }

        private void LobbyCutSscene_OnEndFinish()
        {
            scoreText.SetScore(Lobby.Current.PlayerDatas[(int)OwnerClientId].score);
            scoreText.Show();
        }

        private void Lobby_OnLobbyStateChangedEvent(LobbyState state)
        {
            if (state == LobbyState.MinigameFinished)
                renderTargetPlayerVisual.SetPose(RenderTargetPlayerPoseType.Idle);
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
