using System;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Network
{
    public class HostManager
    {
        public static HostManager Instance = null;
        private bool closed = false;

        public HostManager()
        {
            // 로비를 생성했을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;
        }

        ~HostManager()
        {
            SteamMatchmaking.OnLobbyCreated -= HandleLobbyCreated;

            if(NetworkManager.Singleton == null)
                return;

            NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
            NetworkManager.Singleton.ConnectionApprovalCallback -= HandleConnectionApproval;
        }

        /// <summary>
        /// 호스트로써 클라이언트 시작
        /// </summary>
        public async void StartHost(int maxMember, Action onHostStarted = null)
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnServerStarted += HandleServerStarted;
            networkManager.ConnectionApprovalCallback += HandleConnectionApproval;
            networkManager.StartHost();

            ClientManager.Instance.CurrentLobby = await SteamMatchmaking.CreateLobbyAsync(maxMember);
            ClientManager.Instance.CurrentLobby?.SetData("private", "false");
            ClientManager.Instance.CurrentLobby?.SetData(DEFINE.LOBBY_CLOSED, "false");
            ClientManager.Instance.CurrentLobby?.SetData("owner", SteamClient.Name);
            onHostStarted?.Invoke();
        }

        /// <summary>
        /// 호스트 종료
        /// </summary>
        public void Disconnect()
        {
            if(NetworkManager.Singleton == null)
                return;

            NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
            NetworkManager.Singleton.Shutdown();

            Debug.Log("[Network] Host Disconnected");
        }

        public void OpenLobby()
        {
            closed = false;
            ClientManager.Instance.CurrentLobby?.SetJoinable(true);
            ClientManager.Instance.CurrentLobby?.SetData(DEFINE.LOBBY_CLOSED, "false");
        }

        public void CloseLobby()
        {
            closed = true;
            ClientManager.Instance.CurrentLobby?.SetJoinable(false);
            ClientManager.Instance.CurrentLobby?.SetData(DEFINE.LOBBY_CLOSED, "true");
        }

        #region Netcode Callback

        private void HandleServerStarted()
        {
            Debug.Log($"[Netcode] Host Started");
        }

        private void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            // if(closed)
            // {
            //     response.Reason = "tlqkf dho dkseho";
            //     response.Approved = false;
            // }
            // else
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
