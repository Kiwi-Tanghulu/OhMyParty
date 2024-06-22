using Unity.Collections;
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

        public void BroadcastEvent(ulong instanceID, FixedString128Bytes eventID, NetworkEventParams eventParams)
        {
            BroadcastEventServerRpc(instanceID, eventID, eventParams);
        }

        [ServerRpc]
        private void BroadcastEventServerRpc(ulong instanceID, FixedString128Bytes eventID, NetworkEventParams eventParams)
        {
            BroadcastEventClientRpc(instanceID, eventID, eventParams);
        }

        [ClientRpc]
        private void BroadcastEventClientRpc(ulong instanceID, FixedString128Bytes eventID, NetworkEventParams eventParams)
        {
            NetworkEventTable.GetEvent(instanceID, eventID).Invoke(eventParams);
        }
    }
}
