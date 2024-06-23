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
            // if(eventParams == null)
            //     eventParams = new NetworkEventParams();
            base.Broadcast(eventParams);
        }
    }

    [System.Serializable]
    public class NetworkEvent<T> : UnityEvent<T>, INetworkEvent where T : NetworkEventParams
    {
        private NetworkObject instance = null;

        private ulong eventID = 0;
        ulong INetworkEvent.EventID => eventID;

        public NetworkEvent() : base() {}

        public NetworkEvent(string key) : base()
        {
            this.eventID = NetworkEventTable.StringToHash(key);
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

            NetworkEventPacket packet = new NetworkEventPacket(
                instance.NetworkObjectId, 
                (this as INetworkEvent).EventID, 
                eventParams.GetType().ToString(),
                eventParams.Serialize()
            );
            NetworkEventManager.Instance.BroadcastEvent(packet);
        }

        void INetworkEvent.Invoke(NetworkEventParams eventParams) => Invoke(eventParams as T);
    }
}
