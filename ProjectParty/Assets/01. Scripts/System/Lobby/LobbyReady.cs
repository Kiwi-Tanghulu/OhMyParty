using System;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyReady : LobbyComponent
    {
        public event Action OnLobbyReadyEvent = null;

        public void Ready()
        {
            ReadyServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void ReadyServerRpc(ServerRpcParams rpcParams = default)
        {
            Lobby.ChangePlayerData(rpcParams.Receive.SenderClientId, i => {
                i.isReady = true;
                return i;
            });

            Debug.Log($"[Lobby] Player {rpcParams.Receive.SenderClientId} Set Ready");
            CheckLobbyReady();
        }

        private void CheckLobbyReady()
        {
            bool isLobbyReady = true;
            Lobby.ForEachPlayer(i => {
                Debug.Log($"[Lobby] Player {i.clientID} Ready : {i.isReady}");
                isLobbyReady &= i.isReady;
            });

            if(isLobbyReady)
            {
                OnLobbyReadyEvent?.Invoke();
                Debug.Log($"[Lobby] The Lobby is Ready");
            }
        }
    }
}
