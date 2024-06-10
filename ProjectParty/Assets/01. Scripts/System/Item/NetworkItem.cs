using Unity.Netcode;
using UnityEngine;

namespace OMG.Items
{
    [RequireComponent(typeof(NetworkItemBehaviour))]
    public abstract class NetworkItem : Item
    {
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
            NetworkObject.Spawn(true);
        }

        public override void Active()
        {
            itemBehaviour.ActiveItem();
        }
    }
}
