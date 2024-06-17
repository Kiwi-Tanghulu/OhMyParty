using System;
using Unity.Netcode;
using Steamworks;

namespace OMG.Lobbies
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong ClientID;
        public bool IsReady;
        public int Score;
        public ulong SteamID;
        public int VisualType;

        public string Nickname {
            get {
                if(SteamClient.IsValid)
                    return new Friend(SteamID).Name;
                else
                    return null;
            }
        }

        public PlayerData(ulong clientID, ulong steamID, int visualType)
        {
            ClientID = clientID;
            SteamID = steamID;
            IsReady = false;
            Score = 0;
            VisualType = visualType;
        }

        public PlayerData(ulong clientID)
        {
            this = new PlayerData(clientID, 0, 0);
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
            serializer.SerializeValue(ref SteamID);
            serializer.SerializeValue(ref VisualType);
        }
    }
}
