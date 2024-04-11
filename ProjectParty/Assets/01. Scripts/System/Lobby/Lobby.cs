using System;
using System.Collections.Generic;
using OMG.Extensions;
using OMG.Input;
using OMG.Players;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public partial class Lobby : NetworkBehaviour
    {
        [SerializeField] Player playerPrefab = null;

        public static Lobby Current { get; private set; } = null;

        private Dictionary<Type, LobbyComponent> lobbyComponents = null;
        private NetworkList<PlayerData> players = null;
        public NetworkList<PlayerData> PlayerDatas => players;

        private NetworkVariable<LobbyState> lobbyState = null;
        public LobbyState LobbyState => lobbyState.Value;

        public event Action<LobbyState> OnLobbyStateChangedEvent = null;

        private void Awake()
        {
            LobbyComponent[] components = GetComponents<LobbyComponent>();
            lobbyComponents = components.GetTypeDictionary();
            components.ForEach(i => i.Init(this));

            players = new NetworkList<PlayerData>();
            lobbyState = new NetworkVariable<LobbyState>(LobbyState.Community);
            lobbyState.OnValueChanged += HandleLobbyStateChanged;

            Current = this;
        }

        private void Start()
        {
            InputManager.ChangeInputMap(InputMapType.Play);

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
            Debug.Log($"[Lobby] Player Created ID : {playerData.clientID}");
        }

        private void CreatePlayer(PlayerData playerData)
        {
            players.Add(playerData);
            Player player = Instantiate(playerPrefab, transform);
            player.NetworkObject.Spawn();
            player.NetworkObject.TrySetParent(NetworkObject);
            player.Init(playerData.clientID);
            // something
        }

        public void ChangeLobbyState(LobbyState state)
        {
            if(NetworkManager.Singleton.IsHost == false)
                return;
            
            lobbyState.Value = state;
        }

        public T GetLobbyComponent<T>() where T : LobbyComponent => lobbyComponents[typeof(T)] as T;
    
        private void HandleLobbyStateChanged(LobbyState previousValue, LobbyState newValue)
        {
            OnLobbyStateChangedEvent?.Invoke(newValue);
        }
    }
}
