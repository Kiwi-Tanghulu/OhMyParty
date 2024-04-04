using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG
{
    public class ClientManager
    {
        public static ClientManager Instance = null;

        private FacepunchTransport transport = null;
        public Lobby? CurrentLobby = null;

        public ClientManager(FacepunchTransport transport)
        {
            this.transport = transport;

            // 로비에 참가했을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyEntered += HandleLobbyEntered;

            // 로비에 멤버가 참가하거나 떠났을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyMemberJoined += HandleLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave += HandleLobbyMemberLeave;

            // 누군가가 로비에 나를 초대했을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyInvite += HandleLobbyInvite;

            // 로비 게임이 생성됐을 때 발행되는 이벤트 (아직 잘 모르겠음)
            SteamMatchmaking.OnLobbyGameCreated += HandleLobbyGameCreated;

            // 친구의 로비에 참가를 요청했을 때 발행되는 이벤트 (요청 응답 처리)
            SteamFriends.OnGameLobbyJoinRequested += HandleLobbyJoinRequested;
        }

        ~ClientManager()
        {
            SteamMatchmaking.OnLobbyEntered -= HandleLobbyEntered;

            SteamMatchmaking.OnLobbyMemberJoined -= HandleLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave -= HandleLobbyMemberLeave;

            SteamMatchmaking.OnLobbyInvite -= HandleLobbyInvite;

            SteamMatchmaking.OnLobbyGameCreated -= HandleLobbyGameCreated;

            SteamFriends.OnGameLobbyJoinRequested -= HandleLobbyJoinRequested;

            if(NetworkManager.Singleton == null)
                return;
            
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
        }

        /// <summary>
        /// 게스트로써 클라이언트를 시작
        /// </summary>
        public void StartClient(SteamId id)
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += HandleClientConnected;
            networkManager.OnClientDisconnectCallback += HandleClientDisconnect;

            // 넷코드 클라이언트를 시작할 때 Facepunch 트랜스포트에 Steam ID 세팅
            transport.targetSteamId = id;

            if(networkManager.StartClient())
            {
                Debug.Log($"[Netcode] Client Started");
            }
        }

        /// <summary>
        /// 게스트 종료
        /// </summary>
        public void Disconnect()
        {
            CurrentLobby?.Leave();
            
            if(NetworkManager.Singleton == null)
                return;
            
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
            NetworkManager.Singleton.Shutdown();

            Debug.Log("[Network] Client Disconnected");
        }

        #region Netcode Callback

        private void HandleClientConnected(ulong obj)
        {
            
        }

        private void HandleClientDisconnect(ulong obj)
        {
            
        }

        #endregion

        #region Steamworks Callback

        private void HandleLobbyEntered(Lobby lobby)
        {
            // 로비에 참가했을 때 난 당연히 호스트이면 안 됨
            if(NetworkManager.Singleton.IsHost)
                return;

            // 정상적으로 로비에 참가했다면 넷코드 클라이언트 시작
            StartClient(CurrentLobby.Value.Owner.Id);
        }

        private void HandleLobbyMemberJoined(Lobby lobby, Friend friend)
        {
            Debug.Log($"[Steamworks] Member Joined the Lobby");
        }

        private void HandleLobbyMemberLeave(Lobby lobby, Friend friend)
        {
            Debug.Log($"[Steamworks] Member Left the Lobby");
        }

        private void HandleLobbyInvite(Friend friend, Lobby lobby)
        {
            Debug.Log($"[Steamworks] Lobby Invite From {friend.Name}");
        }

        private void HandleLobbyGameCreated(Lobby lobby, uint ip, ushort port, SteamId id)
        {
            Debug.Log($"[Steamworks] Lobby Game Created");
        }

        private async void HandleLobbyJoinRequested(Lobby lobby, SteamId id)
        {
            // 참가 요청에 대한 응답 받기
            RoomEnter reqResult = await lobby.Join();
            if(reqResult != RoomEnter.Success)
            {
                Debug.Log($"[Steamworks] Failed to Join lobby : {reqResult}");
                return;
            }

            // 참가 되었다면 CurrentLobby 업데이트 해줌
            CurrentLobby = lobby;
        }

        #endregion
    }
}
