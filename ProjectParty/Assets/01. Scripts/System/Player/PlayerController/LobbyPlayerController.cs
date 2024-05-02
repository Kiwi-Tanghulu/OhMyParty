using OMG.Lobbies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace OMG.Player
{
    public class LobbyPlayerController : PlayerController
    {
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Lobby.Current.PlayerContainer.RegistPlayer(this);
            Debug.Log(Lobby.Current.PlayerContainer.Count);
            Lobby.Current.PlayerContainer.UnregistPlayer(this);
            Debug.Log(Lobby.Current.PlayerContainer.Count);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            Lobby.Current.PlayerContainer.UnregistPlayer(this);
        }
    }
}
