using System;
using Unity.Netcode;

namespace OMG.Lobbies
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong clientID;
        public bool isReady;
        public int score;

        public PlayerData(ulong clientID)
        {
            this.clientID = clientID;
            isReady = false;
            score = 0;
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
            serializer.SerializeValue(ref score);
        }
    }
}