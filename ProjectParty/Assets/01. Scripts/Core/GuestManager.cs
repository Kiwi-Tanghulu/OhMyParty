using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Network
{
    public class GuestManager
    {
        public static GuestManager Instance = null;

        private FacepunchTransport transport = null;

        public GuestManager(FacepunchTransport transport)
        {
            this.transport = transport;

            // 로비에 참가했을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyEntered += HandleLobbyEntered;
        }

        ~GuestManager()
        {
            SteamMatchmaking.OnLobbyEntered -= HandleLobbyEntered;

            if(NetworkManager.Singleton == null)
                return;
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
        }

        /// <summary>
        /// 게스트 시작
        /// </summary>
        public void StartGuest(SteamId id)
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += HandleClientConnected;
            networkManager.OnClientDisconnectCallback += HandleClientDisconnect;

            // 넷코드 클라이언트를 시작할 때 Facepunch 트랜스포트에 Steam ID 세팅
            transport.targetSteamId = id;

            if(networkManager.StartClient())
            {
                Debug.Log($"[Netcode] Guest Started");
            }
        }

        /// <summary>
        /// 게스트 종료
        /// </summary>
        public void Disconnect()
        {
            ClientManager.Instance.CurrentLobby?.Leave();
            
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
            
        }

        #endregion

        #region Steamworks Callback
        
        private void HandleLobbyEntered(Lobby lobby)
        {
            // 로비에 참가했을 때 난 당연히 호스트이면 안 됨
            if(NetworkManager.Singleton.IsHost)
                return;

            // 정상적으로 로비에 참가했다면 넷코드 클라이언트 시작
            StartGuest(ClientManager.Instance.CurrentLobby.Value.Owner.Id);
        }

        #endregion   
    }
}
