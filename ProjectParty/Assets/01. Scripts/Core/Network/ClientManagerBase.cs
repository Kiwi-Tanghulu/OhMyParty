using System;
using Unity.Netcode;

namespace OMG.Networks
{
    public class ClientManager
    {
        public static ClientManager Instance = null;

        private INetworkLobby currentLobby = null;
        public INetworkLobby CurrentLobby => currentLobby;

        public NetworkTransport NetworkTransport = null;
        public ulong ClientID => NetworkManager.Singleton.LocalClientId;

        public event Action OnConnectedEvent = null;
        public event Action OnDisconnectedEvent = null;

        public void Init(INetworkLobby networkLobby)
        {
            currentLobby = networkLobby;
            OnConnectedEvent?.Invoke();
        }

        public void Release()
        {
            currentLobby = null;
            OnDisconnectedEvent?.Invoke();
        }
    }
}
