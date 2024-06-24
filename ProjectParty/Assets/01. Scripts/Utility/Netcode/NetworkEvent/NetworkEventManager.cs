using Unity.Netcode;

namespace OMG.NetworkEvents
{
    internal class NetworkEventManager : NetworkBehaviour
    {
        private static NetworkEventManager instance = null;
        public static NetworkEventManager Instance => instance;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void BroadcastEvent(NetworkEventPacket packet)
        {
            BroadcastEventServerRpc(packet);
        }

        [ServerRpc]
        private void BroadcastEventServerRpc(NetworkEventPacket packet)
        {
            BroadcastEventClientRpc(packet);
        }

        [ClientRpc]
        private void BroadcastEventClientRpc(NetworkEventPacket packet)
        {
            NetworkEventParams eventParams = NetworkEventTable.GetEventParams(packet.ParamsID, packet.Buffer);
            NetworkEventTable.GetEvent(packet.InstanceID, packet.EventID).Invoke(eventParams);
        }
    }
}
