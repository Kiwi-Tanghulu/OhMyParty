using Cinemachine;
using OMG.Extensions;
using OMG.UI.Minigames.Deathmatches;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace OMG.Minigames
{
    public class DeathmatchCycle : MinigameCycle
    {
        [SerializeField] int[] scoreWeight = { 50, 100, 500, 1000 };

        [Space(15f)]
        [SerializeField] UnityEvent<ulong> OnPlayerDeadEvent = null;
        private PlayerPanel playerPanel = null;

        private int deadPlayerCount = 0;
        public int DeadPlayerCount => deadPlayerCount;

        protected override void Awake()
        {
            base.Awake();
            playerPanel = DEFINE.MinigameCanvas.Find("TopPanel/PlayerPanel").GetComponent<PlayerPanel>();
        }

        protected virtual void Start()
        {
            playerPanel.Init();
        }

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

                Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Dead Player Count : {deadPlayerCount}");
                if ((minigame.PlayerDatas.Count - deadPlayerCount) <= 1)
                    FinishCycle();
            }

            minigame.PlayerDatas.ForEach((i, index) => {
                if(i.clientID == clientID)
                    playerPanel.SetDead(index);
            });

            OnPlayerDeadEvent?.Invoke(clientID);
        }

        public virtual void FinishCycle()
        {
            minigame.FinishGame();
        }
    }
}
