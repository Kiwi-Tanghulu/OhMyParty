using System;
using Unity.Netcode;
using Steamworks;

namespace OMG.Lobbies
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong clientID;
        public bool isReady;
        public int score;
        public Friend steamClient;
        public string Name {
            get {
                #if STEAMWORKS
                return steamClient.Name;
                #else
                return null;
                #endif
            }
        }

        public PlayerData(ulong clientID, SteamId steamID)
        {
            this.clientID = clientID;
            steamClient = new Friend(steamID);
            isReady = false;
            score = 0;
        }

        public PlayerData(ulong clientID)
        {
            this = new PlayerData(clientID, new SteamId());
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
