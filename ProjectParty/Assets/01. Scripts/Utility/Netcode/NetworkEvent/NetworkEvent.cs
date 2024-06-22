using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.NetworkEvents
{
    [System.Serializable]
    public class NetworkEvent : NetworkEvent<NetworkEventParams>
    {
        public NetworkEvent() : base() {}
        public NetworkEvent(string eventID) : base(eventID) { }

        public override void Broadcast(NetworkEventParams eventParams = null)
        {
            base.Broadcast(new NetworkEventParams());
        }
    }

    [System.Serializable]
    public class NetworkEvent<T> : UnityEvent<T>, INetworkEvent where T : NetworkEventParams
    {
        private NetworkObject instance = null;

        private FixedString128Bytes eventID = "";
        FixedString128Bytes INetworkEvent.EventID => eventID;

        public NetworkEvent() : base() {}

        public NetworkEvent(string eventID) : base()
        {
            this.eventID = eventID;
        }

        ~NetworkEvent()
        {
            Unregister();
        }

        public void Register(NetworkObject instance)
        {
            this.instance = instance;
            NetworkEventTable.RegisterEvent(instance.NetworkObjectId, this);
        }

        public void Unregister()
        {
            NetworkEventTable.UnregisterEvent(instance.NetworkObjectId, this);
        }

        public virtual void Broadcast(T eventParams)
        {
            if(instance.IsSpawned == false)
            {
                Debug.LogError("Network Object Instance Does Not Spawned");
                return;
            }

            if(instance.IsOwner == false)
            {
                Debug.LogError("Only Owner Can Broadcast Network Event");
                return;
            }

            NetworkEventManager.Instance.BroadcastEvent(instance.NetworkObjectId, (this as INetworkEvent).EventID, eventParams);
        }

        void INetworkEvent.Invoke(NetworkEventParams eventParams) => Invoke(eventParams as T);
    }
}
