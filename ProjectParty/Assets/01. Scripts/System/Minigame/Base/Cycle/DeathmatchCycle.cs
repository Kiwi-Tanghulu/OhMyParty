using OMG.Extensions;
using OMG.UI.Minigames;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class DeathmatchCycle : MinigameCycle
    {
        [SerializeField] int[] scoreWeight = { 50, 100, 500, 1000 };

        [Space(15f)]
        [SerializeField] UnityEvent<ulong> OnPlayerDeadEvent = null;
        private DeathmatchPlayerPanel playerPanel = null;
        private PlayableMinigame playableMinigame = null;

        private int deadPlayerCount = 0;
        public int DeadPlayerCount => deadPlayerCount;

        protected override void Awake()
        {
            base.Awake();
            playerPanel = minigame.MinigamePanel.PlayerPanel as DeathmatchPlayerPanel;
            playableMinigame = minigame as PlayableMinigame;
        }

        public virtual void HandlePlayerDead(ulong clientID)
        {
            int score = scoreWeight[deadPlayerCount];
            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                bool isDead = data.lifeCount <= 1;
                if(isDead)
                    playerPanel.SetDead(index);

                if(IsHost)
                {
                    data.lifeCount -= 0;

                    if(isDead)
                    {
                        data.score = score;
                        deadPlayerCount++;

                        Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Dead Player Count : {deadPlayerCount}");
                        if ((minigame.PlayerDatas.Count - deadPlayerCount) <= 1)
                            FinishCycle();
                    }
                    else
                        Respawn(clientID);
                    
                    minigame.PlayerDatas[index] = data;
                }
            });

            OnPlayerDeadEvent?.Invoke(clientID);
        }

        public virtual void FinishCycle()
        {
            minigame.PlayerDatas.ChangeData(i => i.IsDead == false, data => {
                data.score = scoreWeight[deadPlayerCount];
                return data;
            });
            minigame.FinishGame();
        }

        public void Respawn(ulong clientID)
        {
            playableMinigame.RespawnPlayer(clientID);
        }
    }
}
