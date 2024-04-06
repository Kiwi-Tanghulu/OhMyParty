using System;
using System.Collections.Generic;
using OMG.Player;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class Lobby : NetworkBehaviour
    {
        private Dictionary<Type, LobbyComponent> lobbyComponents = null;

        private NetworkList<PlayerData> players = null;

        private void Awake()
        {
            LobbyComponent[] components = GetComponents<LobbyComponent>();
            components.ForEach(i => i.Init(this));
            lobbyComponents = components.GetTypeDictionary();

            players = new NetworkList<PlayerData>();
        }

        private void Start()
        {
            // 모든 처리가 끝난 후 로비씬을 들어오게 되기 때문에 스타트에서 해줘도 됨
            PlayerJoinServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void PlayerJoinServerRpc(ServerRpcParams rpcParams = default)
        {
            PlayerData playerData = new PlayerData(rpcParams.Receive.SenderClientId);
            if(players.Contains(playerData))
                return;

            CreatePlayer(playerData);
            Debug.Log(players.Count);
        }

        private void CreatePlayer(PlayerData playerData)
        {
            players.Add(playerData);
            // something
        }

        public T GetLobbyComponent<T>() where T : LobbyComponent => lobbyComponents[typeof(T)] as T;
        public bool GetPlayerData(ulong clientID, out PlayerData playerData)
        {
            playerData = default;

            for(int i = 0; i < players.Count; ++i)
            {
                if(players[i].clientID == clientID)
                {
                    playerData = players[i];
                    return true;
                }
            }

            return false;
        }
    }
}
