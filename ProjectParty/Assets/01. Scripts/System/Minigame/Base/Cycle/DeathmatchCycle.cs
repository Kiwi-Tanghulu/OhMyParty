using OMG.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class DeathmatchCycle : MinigameCycle
    {
        [SerializeField] int[] scoreWeight = { 50, 100, 500, 1000 };

        [Space(15f)]
        [SerializeField] UnityEvent<ulong> OnPlayerDeadEvent = null;

        private int deadPlayerCount = 0;
        public int DeadPlayerCount => deadPlayerCount;

        public virtual void HandlePlayerDead(ulong clientID)
        {
            if(IsHost)
            {
                deadPlayerCount++;
                
                minigame.PlayerDatas.ChangeData(i => i.clientID == clientID, data => {
                    data.isDead = true;
                    data.score = scoreWeight[deadPlayerCount];
                    return data;
                });

                if ((minigame.PlayerDatas.Count - deadPlayerCount) == 1)
                    FinishCycle();
            }

            OnPlayerDeadEvent?.Invoke(clientID);
        }

        public virtual void FinishCycle()
        {
            minigame.FinishGame();
        }
    }
}
