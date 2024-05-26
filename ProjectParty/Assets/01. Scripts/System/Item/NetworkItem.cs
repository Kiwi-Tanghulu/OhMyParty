using Unity.Netcode;
using UnityEngine;

namespace OMG.Items
{
    [RequireComponent(typeof(NetworkItemBehaviour))]
    public abstract class NetworkItem : Item
    {
        protected NetworkItemBehaviour itemBehaviour = null;
        public NetworkObject NetworkObject => itemBehaviour.NetworkObject;

        protected virtual void Awake()
        {
            itemBehaviour = GetComponent<NetworkItemBehaviour>();
        }

        public override void Active()
        {
            itemBehaviour.ActiveItem();
        }
    }
}
