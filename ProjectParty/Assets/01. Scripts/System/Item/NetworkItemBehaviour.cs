using Unity.Netcode;

namespace OMG.Items
{
    public class NetworkItemBehaviour : NetworkBehaviour
    {
        private NetworkItem item = null;

        private void Awake()
        {
            item = GetComponent<NetworkItem>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            item.OnSpawnedEvent?.Invoke();
        }

        public void ActiveItem()
        {
            ActiveServerRpc();
        }

        [ServerRpc]
        private void ActiveServerRpc()
        {
            ActiveClientRpc();
        }

        [ClientRpc]
        private void ActiveClientRpc()
        {
            item.OnActive();
            item.OnActiveEvent?.Invoke();
        }
    }
}
