using System;
using Unity.Netcode;
using Steamworks;
using Unity.Collections;

namespace OMG.Lobbies
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong ClientID;
        public bool IsReady;
        public int Score;
        public FixedString32Bytes Nickname;

        public PlayerData(ulong clientID, FixedString32Bytes nickname)
        {
            ClientID = clientID;
            Nickname = nickname;
            IsReady = false;
            Score = 0;
        }

        public PlayerData(ulong clientID)
        {
            this = new PlayerData(clientID, null);
        }

        public bool Equals(PlayerData other)
        {
            bool equal = ClientID == other.ClientID;
            return equal;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ClientID);
            serializer.SerializeValue(ref IsReady);
            serializer.SerializeValue(ref Score);
        }
    }
}
