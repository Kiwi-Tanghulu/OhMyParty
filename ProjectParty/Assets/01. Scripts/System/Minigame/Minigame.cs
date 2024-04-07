using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        [SerializeField] MinigameSO minigameData = null;
        public MinigameSO MinigameData => minigameData;

        protected NetworkList<PlayerData> players = null;
        public NetworkList<PlayerData> JoinedPlayers => players;

        protected virtual void Awake()
        {
            players = new NetworkList<PlayerData>();
        }

        public virtual void Init(params ulong[] playerIDs)
        {
            for(int i = 0; i < playerIDs.Length; ++i)
                players.Add(new PlayerData(playerIDs[i]));
        }

        public abstract void StartGame();
        public abstract void FinishGame();
    }
}
