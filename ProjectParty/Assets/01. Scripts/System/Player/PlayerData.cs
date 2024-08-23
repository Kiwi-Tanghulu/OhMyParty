using System;
using Unity.Collections;
using Unity.Netcode;

namespace OMG.Lobbies
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong ClientID;
        public bool IsReady;
        public int Score;
        public int VisualType;

        private FixedString64Bytes nickname;
        public string Nickname => nickname.ToString();

        public PlayerData(ulong clientID, FixedString64Bytes nickname, int visualType)
        {
            ClientID = clientID;
            IsReady = false;
            Score = 0;
            VisualType = visualType;
            this.nickname = nickname;
        }

        public PlayerData(ulong clientID)
        {
            this = new PlayerData(clientID, "unknown", 0);
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
            serializer.SerializeValue(ref VisualType);
            serializer.SerializeValue(ref nickname);
        }
    }
}
