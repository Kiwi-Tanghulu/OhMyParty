using OMG.Extensions;
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

        protected MinigameCycle cycle = null;

        protected virtual void Awake()
        {
            players = new NetworkList<PlayerData>();
            cycle = GetComponent<MinigameCycle>();
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>  
        public virtual void Init(params ulong[] playerIDs)
        {
            for(int i = 0; i < playerIDs.Length; ++i)
                players.Add(new PlayerData(playerIDs[i]));

            StartIntro();
        }

        /// <summary>
        /// Only Host Could Call this Method

        /// </summary>
        public virtual void Release() {}

        public virtual void StartIntro()
        {
            StartCoroutine(this.DelayCoroutine(minigameData.IntroPostponeTime, cycle.PlayIntro));
        }

        public virtual void StartOutro()
        {
            StartCoroutine(this.DelayCoroutine(minigameData.OutroPostponeTime, cycle.PlayOutro));
        }

        public virtual void StartGame() {}

        public virtual void FinishGame() 
        {
            StartOutro();
        }
    }
}
