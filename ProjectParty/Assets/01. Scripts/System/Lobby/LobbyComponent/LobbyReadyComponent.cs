using System;
using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyReadyComponent : LobbyComponent
    {
        public event Action OnLobbyReadyEvent = null;
        public event Action<ulong> OnPlayerReadyEvent = null;

        public void Ready(ulong clientID)
        {
            ReadyServerRpc(clientID);
        }

        [ServerRpc(RequireOwnership = false)]
        private void ReadyServerRpc(ulong clientID)
        {
            Lobby.PlayerDatas.ChangeData(i => i.ClientID == clientID, data => {
                data.IsReady = true;
                BroadcastReadyClientRpc(clientID);
                return data;
            });

            Debug.Log($"[Lobby] Player {clientID} Set Ready");
            CheckLobbyReady();
        }

        [ClientRpc]
        private void BroadcastReadyClientRpc(ulong clientID)
        {
            OnPlayerReadyEvent?.Invoke(clientID);
        }

        private void CheckLobbyReady()
        {
            bool isLobbyReady = true;
            Lobby.PlayerDatas.ForEach(i => {
                isLobbyReady &= i.IsReady;
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
                data.IsReady = false;
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
