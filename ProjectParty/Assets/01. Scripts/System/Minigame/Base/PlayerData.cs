using System;
using Unity.Netcode;

namespace OMG.Minigames
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong clientID;
        public int score;
        public int lifeCount;
        public bool isSkipCutscene;
        public bool IsDead => lifeCount <= 0;

        public PlayerData(ulong id)
        {
            clientID = id;
            score = 0;
            isSkipCutscene = false;
            lifeCount = 1;
        }

        public bool Equals(PlayerData other)
        {
            return clientID == other.clientID;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientID);
            serializer.SerializeValue(ref score);
            serializer.SerializeValue(ref lifeCount);
            serializer.SerializeValue(ref isSkipCutscene);
        }
    }
}
