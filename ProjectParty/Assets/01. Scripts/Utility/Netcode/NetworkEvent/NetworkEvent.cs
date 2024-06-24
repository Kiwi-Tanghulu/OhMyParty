using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.NetworkEvents
{
    [System.Serializable]
    public class NetworkEvent : NetworkEvent<NoneParams>
    {
        public NetworkEvent() : base() {}
        public NetworkEvent(string eventID) : base(eventID) { }

        public void Alert(bool requireOwnership = true)
        {
            NoneParams eventParams = new NoneParams();
            Alert(eventParams, requireOwnership);
        }

        public void Broadcast(bool requireOwnership = true)
        {
            NoneParams eventParams = new NoneParams();
            Broadcast(eventParams, requireOwnership);
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
            eventID = NetworkEventTable.StringToHash(key);
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

        public virtual void Alert(T eventParams, bool requireOwnership = true)
        {
            if(Middleware(requireOwnership) == false)
                return;

            NetworkEventPacket packet = CreatePacket(eventParams);
            NetworkEventManager.Instance.AlertEvent(packet);
        }

        public virtual void Broadcast(T eventParams, bool requireOwnership = true)
        {
            if(Middleware(requireOwnership) == false)
                return;

            NetworkEventPacket packet = CreatePacket(eventParams);
            NetworkEventManager.Instance.BroadcastEvent(packet);
        }

        private bool Middleware(bool requireOwnership)
        {
            if(instance.IsSpawned == false)
            {
                Debug.LogError("Network Object Instance Does Not Spawned");
                return false;
            }

            if(requireOwnership && instance.IsOwner == false)
            {
                Debug.LogError("Only Owner Can Broadcast Network Event");
                return false;
            }

            return true;
        }

        private NetworkEventPacket CreatePacket(T eventParams)
        {
            NetworkEventPacket packet = new NetworkEventPacket(
                instance.NetworkObjectId, 
                (this as INetworkEvent).EventID, 
                eventParams.GetType().ToString(),
                eventParams.Serialize()
            );

            return packet;
        }

        void INetworkEvent.Invoke(NetworkEventParams eventParams) => Invoke(eventParams as T);
    }
}
