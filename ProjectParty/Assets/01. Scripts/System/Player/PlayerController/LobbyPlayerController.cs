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
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            Lobby.Current.PlayerContainer.UnregistPlayer(this);
        }
    }
}
