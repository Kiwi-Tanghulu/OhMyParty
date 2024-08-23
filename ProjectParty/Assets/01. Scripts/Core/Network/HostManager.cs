using System;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Network
{
    public class HostManager_
    {
        public static HostManager_ Instance = null;
        // private bool closed = false;

        public event Action<ulong> OnClientDisconnectedEvent = null;
        public bool Alive { get; private set; } = false;

        public HostManager_()
        {
            // 로비를 생성했을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;
        }

        public void Release()
        {
            SteamMatchmaking.OnLobbyCreated -= HandleLobbyCreated;

            if (NetworkManager.Singleton == null)
                return;

            NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
            NetworkManager.Singleton.ConnectionApprovalCallback -= HandleConnectionApproval;

            OnClientDisconnectedEvent = null;
        }

        /// <summary>
        /// 호스트로써 클라이언트 시작
        /// </summary>
        public async void StartHost(int maxMember, Action onHostStarted = null)
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnServerStarted += HandleServerStarted;
            networkManager.OnClientDisconnectCallback += HandleClientDisconnect;
            networkManager.ConnectionApprovalCallback += HandleConnectionApproval;
            networkManager.StartHost();

            ClientManager_.Instance.CurrentLobby = await SteamMatchmaking.CreateLobbyAsync(maxMember);
            ClientManager_.Instance.CurrentLobby?.SetData("private", "false");
            // ClientManager.Instance.CurrentLobby?.SetData(DEFINE.LOBBY_CLOSED, "false");
            ClientManager_.Instance.CurrentLobby?.SetData("owner", SteamClient.Name);
            onHostStarted?.Invoke();

            Alive = true;
        }

        /// <summary>
        /// 호스트 종료
        /// </summary>
        public void Disconnect()
        {
            Alive = false;
            
            if(NetworkManager.Singleton == null)
                return;

            NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
            NetworkManager.Singleton.ConnectionApprovalCallback = null;
            NetworkManager.Singleton.Shutdown();

            Debug.Log("[Network] Host Disconnected");
        }

        public void OpenLobby()
        {
            ClientManager_.Instance.CurrentLobby?.SetJoinable(true);
        }

        public void CloseLobby()
        {
            ClientManager_.Instance.CurrentLobby?.SetJoinable(false);
        }

        #region Netcode Callback

        private void HandleServerStarted()
        {
            Debug.Log($"[Netcode] Host Started");
        }

        private void HandleClientDisconnect(ulong clientID)
        {
            if(NetworkManager.Singleton == null || NetworkManager.Singleton.IsHost == false)
                return;

            OnClientDisconnectedEvent?.Invoke(clientID);
        }

        private void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = true;
            response.CreatePlayerObject = false;
        }

        #endregion

        #region Steamworks Callback

        private void HandleLobbyCreated(Result result, Lobby lobby)
        {
            if(result != Result.OK)
            {
                Debug.Log($"[Steamworks] Lobby doesn't Created");
                return;
            }

            // 로비가 정상적으로 생성되었다면 각종 설정을 해줌
            lobby.SetPublic();
            lobby.SetJoinable(true);

            // 각 클라이언트가 하나 하나 서버가 됨. P2P이기 때문. 이 때 해당 로비의 서버를 로비를 생성한 오너의 서버로 두겠다는 것
            // Steammatchmaking.OnLobbyGameCreated 가 호출됨
            lobby.SetGameServer(lobby.Owner.Id);
        }

        #endregion
    }
}
