using System;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Networks
{
    public abstract class HostManager
    {
        private static HostManager instance = null;
        public static HostManager Instance => instance;

        public event Action<ulong> OnClientConnectedEvent = null;
        public event Action<ulong> OnClientDisconnectEvent = null;

        public bool Alive { get; private set; } = false;

        public void Init()
        {
            instance = this;
        }

        #region Start Host

        public async void StartHost(int maxMember, Action onHostStarted = null)
        {
            bool response = OnBeginStartHost();
            if(response == false)
            {
                Debug.LogWarning("$[Network] Failed to Begining Start Host");
                return;
            }

            INetworkLobby networkLobby = await CreateLobby(maxMember);
            if(networkLobby == null)
            {
                Debug.LogWarning("$[Network] Failed to Create Lobby");
                return;
            }
            
            response = OnHostStarted(networkLobby);
            if(response == false)
            {
                Debug.LogWarning("$[Network] Failed to Start Host");
                return;
            }

            response = InitNetworkManager();
            if(response == false)
            {
                Debug.LogWarning("$[Network] Failed to Init NetworkManager");
                return;
            }

            ClientManager.Instance.Init(networkLobby);
            Alive = true;

            onHostStarted?.Invoke();

            Debug.Log("$[Network] Host Started");
        }

        private bool InitNetworkManager()
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += HandleClientConnected;
            networkManager.OnClientDisconnectCallback += HandleClientDisconnect;
            networkManager.ConnectionApprovalCallback += HandleConnectionApproval;
            
            return networkManager.StartHost();
        }

        protected abstract Task<INetworkLobby> CreateLobby(int maxMember);
        protected virtual bool OnBeginStartHost() { return true; }
        protected virtual bool OnHostStarted(INetworkLobby networkLobby) { return true; }

        #endregion

        #region Close Host
        
        public virtual void Disconnect()
        {
            if(Alive == false || ClientManager.Instance.CurrentLobby == null)
                return;

            ClientManager.Instance.CurrentLobby?.Leave();
            ClientManager.Instance.Release();

            Alive = false;
            
            if(NetworkManager.Singleton == null)
                return;
            
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
            NetworkManager.Singleton.ConnectionApprovalCallback = null;
            NetworkManager.Singleton.Shutdown();

            Debug.Log("[Network] Host Disconnected");
        }

        #endregion

        #region NetworkManager Callback

        protected virtual void HandleClientConnected(ulong clientID)
        {
            OnClientConnectedEvent?.Invoke(clientID);
        }

        protected virtual void HandleClientDisconnect(ulong clientID)
        {
            if(NetworkManager.Singleton == null || NetworkManager.Singleton.IsHost == false)
                return;

            OnClientDisconnectEvent?.Invoke(clientID);
        }

        protected virtual void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = true;
            response.CreatePlayerObject = false;
        }

        #endregion
    }
}
