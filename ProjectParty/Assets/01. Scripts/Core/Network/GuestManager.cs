using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Network
{
    public class GuestManager_
    {
        public static GuestManager_ Instance = null;

        private FacepunchTransport transport = null;
        public bool Alive { get; private set; } = false;

        public GuestManager_(FacepunchTransport transport)
        {
            this.transport = transport;

            // // 로비에 참가했을 때 발행되는 이벤트
            // SteamMatchmaking.OnLobbyEntered += HandleLobbyEntered;
        }

        public void Release()
        {
            // SteamMatchmaking.OnLobbyEntered -= HandleLobbyEntered;

            if (NetworkManager.Singleton == null)
                return;
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
        }

        public async void StartGuest(Lobby lobby)
        {
            // 참가 요청에 대한 응답 받기
            RoomEnter reqResult = await lobby.Join();
            if(reqResult != RoomEnter.Success)
            {
                Debug.Log($"[Steamworks] Failed to Join lobby : {reqResult}");
                return;
            }

            // 참가 되었다면 CurrentLobby 업데이트 해줌
            ClientManager_.Instance.CurrentLobby = lobby;
            InitTransport(lobby.Owner.Id);
        }

        /// <summary>
        /// 트랜스포트 초기화
        /// </summary>
        private void InitTransport(SteamId id)
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += HandleClientConnected;
            networkManager.OnClientDisconnectCallback += HandleClientDisconnect;

            // 넷코드 클라이언트를 시작할 때 Facepunch 트랜스포트에 Steam ID 세팅
            transport.targetSteamId = id;

            if(networkManager.StartClient())
            {
                Debug.Log($"[Netcode] Guest Started");
                Alive = true;
            }
        }

        /// <summary>
        /// 게스트 종료
        /// </summary>
        public void Disconnect()
        {
            ClientManager_.Instance.CurrentLobby?.Leave();
            Alive = false;
            
            if(NetworkManager.Singleton == null)
                return;
            
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
            NetworkManager.Singleton.Shutdown();

            Debug.Log("[Network] Guest Disconnected");
        }

        #region Netcode Callback

        private void HandleClientConnected(ulong obj)
        {
            
        }

        private void HandleClientDisconnect(ulong obj)
        {
            Debug.Log(NetworkManager.Singleton.DisconnectReason);
        }

        #endregion 
    }
}
