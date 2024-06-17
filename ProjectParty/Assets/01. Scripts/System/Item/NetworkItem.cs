using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Items
{
    [RequireComponent(typeof(NetworkItemBehaviour))]
    public abstract class NetworkItem : Item
    {
        public UnityEvent OnSpawnedEvent = new UnityEvent();

        protected NetworkItemBehaviour itemBehaviour = null;
        public NetworkObject NetworkObject => itemBehaviour.NetworkObject;
        public bool IsHost => itemBehaviour.IsHost;
        public bool IsOwner => itemBehaviour.IsOwner;

        protected virtual void Awake()
        {
            itemBehaviour = GetComponent<NetworkItemBehaviour>();
        }

        public virtual void Init()
        {
            Spawn();
        }

        public override void Active()
        {
            itemBehaviour.ActiveItem();
        }

        public void Spawn()
        {
            SpawnServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnServerRpc()
        {
            NetworkObject.Spawn(true);
        }
    }
}
