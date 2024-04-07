using System;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong clientID;
        public int score;

        public PlayerData(ulong id)
        {
            clientID = id;
            score = 0;
        }

        public bool Equals(PlayerData other)
        {
            return clientID == other.clientID;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientID);
            serializer.SerializeValue(ref score);
        }
    }
}
