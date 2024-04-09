using System;
using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyReadyComponent : LobbyComponent
    {
        public event Action OnLobbyReadyEvent = null;

        public void Ready()
        {
            ReadyServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void ReadyServerRpc(ServerRpcParams rpcParams = default)
        {
            ulong id = rpcParams.Receive.SenderClientId;
            Lobby.PlayerDatas.ChangeData(i => i.clientID == id, data => {
                data.isReady = true;
                return data;
            });

            Debug.Log($"[Lobby] Player {rpcParams.Receive.SenderClientId} Set Ready");
            CheckLobbyReady();
        }

        private void CheckLobbyReady()
        {
            bool isLobbyReady = true;
            Lobby.PlayerDatas.ForEach(i => {
                isLobbyReady &= i.isReady;
            });

            if(isLobbyReady)
            {
                HandleLobbyReadyClientRpc();
                Debug.Log($"[Lobby] The Lobby is Ready");
            }
        }

        public void ClearLobbyReady()
        {
            Lobby.PlayerDatas.ChangeAllData(data => {
                data.isReady = false;
                return data;
            });
        }

        [ClientRpc]
        private void HandleLobbyReadyClientRpc()
        {
            OnLobbyReadyEvent?.Invoke();
        }
    }
}
