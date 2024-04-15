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

        public void HandlePlayerDead(ulong clientID)
        {
            deadPlayerCount++;
            minigame.PlayerDatas.ChangeData(i => i.clientID == clientID, data => {
                data.isDead = true;
                data.score = scoreWeight[deadPlayerCount];
                return data;
            });

            OnPlayerDeadEvent?.Invoke(clientID);

            if((minigame.PlayerDatas.Count - deadPlayerCount) == 1)
                FinishCycle();
        }

        public void FinishCycle()
        {
            minigame.FinishGame();
        }
    }
}
