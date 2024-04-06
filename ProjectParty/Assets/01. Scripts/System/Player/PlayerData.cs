using System;
using Unity.Netcode;

namespace OMG.Player
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong clientID;
        public bool isReady;

        public PlayerData(ulong clientID)
        {
            this.clientID = clientID;
            isReady = false;
        }

        public bool Equals(PlayerData other)
        {
            bool equal = clientID == other.clientID;
            return equal;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientID);
            serializer.SerializeValue(ref isReady);
        }
    }
}
