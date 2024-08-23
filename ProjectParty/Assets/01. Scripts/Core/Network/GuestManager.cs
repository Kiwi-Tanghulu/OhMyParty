using System;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Networks
{
    public abstract class GuestManager
    {
        private static GuestManager instance = null;
        public static GuestManager Instance => instance;

        public event Action<ulong> OnClientConnectedEvent = null;
        public event Action<ulong> OnClientDisconnectEvent = null;

        public bool Alive { get; private set; } = false;

        public void Init()
        {
            instance = this;
        }

        public abstract Task<INetworkLobby[]> GetLobbyListAsync(int count = 5);

        #region Start Guest
        public async void StartGuest(INetworkLobby networkLobby, Action onGuestStarted = null)
        {
            bool response = OnBeginStartGuest();
            if (response == false)
            {
                Debug.LogWarning("$[Network] Failed to Begining Start Guest");
                return;
            }

            response = await networkLobby.Join();
            if(response == false)
            {
                Debug.LogWarning("$[Network] Failed to Join Lobby");
                return;
            }

            response = OnGuestStarted(networkLobby);
            if(response == false)
            {
                Debug.LogWarning("$[Network] Failed to Start Guest");
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

            onGuestStarted?.Invoke();

            Debug.Log("$[Network] Guest Started");
        }

        private bool InitNetworkManager()
        {
            NetworkManager networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += HandleClientConnected;
            networkManager.OnClientDisconnectCallback += HandleClientDisconnect;

            return networkManager.StartClient();
        }

        protected virtual bool OnBeginStartGuest() { return true; }
        protected virtual bool OnGuestStarted(INetworkLobby networkLobby) { return true; }

        #endregion

        #region Close Guest

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
            NetworkManager.Singleton.Shutdown();

            Debug.Log("[Network] Guest Disconnected");
        }

        #endregion

        #region NetworkManager Callback

        protected virtual void HandleClientConnected(ulong clientID)
        {
            OnClientConnectedEvent?.Invoke(clientID);
        }

        protected virtual void HandleClientDisconnect(ulong clientID)
        {
            if(NetworkManager.Singleton == null || NetworkManager.Singleton.IsHost == true)
                return;

            OnClientDisconnectEvent?.Invoke(clientID);
        }

        #endregion
    }
}
