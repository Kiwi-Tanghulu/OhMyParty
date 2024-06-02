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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Lobby.Current.PlayerContainer.RegistPlayer(this);
            Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] += LobbyCutSscene_OnEndFinish;

            PlayerManager.Instance.CreatePlayerRenderTarget(this);
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
    }
}
