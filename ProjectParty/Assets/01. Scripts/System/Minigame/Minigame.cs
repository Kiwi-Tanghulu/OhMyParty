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

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>  
        public virtual void Init(params ulong[] playerIDs)
        {
            for(int i = 0; i < playerIDs.Length; ++i)
                players.Add(new PlayerData(playerIDs[i]));
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>  
        public abstract void StartGame();
        
        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>
        public abstract void FinishGame();
    }
}
