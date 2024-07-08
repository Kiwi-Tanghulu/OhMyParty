using System;
using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.UI.Minigames;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class DeathmatchCycle : MinigameCycle
    {
        [SerializeField] int[] scoreWeight = { 50, 100, 500, 1000 };

        [Space(15f)]
        [SerializeField] NetworkEvent<UlongParams> onPlayerDeadEvent = new NetworkEvent<UlongParams>("PlayerDead");
        private DeathmatchPlayerPanel playerPanel = null;
        private PlayableMinigame playableMinigame = null;

        private int deadPlayerCount = 0;
        public int DeadPlayerCount => deadPlayerCount;

        private float lastDecisionTime = 0f;
        private int lastScore = 0;

        protected override void Awake()
        {
            base.Awake();
            playerPanel = minigame.MinigamePanel.PlayerPanel as DeathmatchPlayerPanel;
            playableMinigame = minigame as PlayableMinigame;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onPlayerDeadEvent.AddListener(HandlePlayerDead);
            onPlayerDeadEvent.Register(NetworkObject);
        }

        public virtual void SetPlayerDead(ulong clientID)
        {
            onPlayerDeadEvent?.Broadcast(clientID);
        }

        private void HandlePlayerDead(UlongParams clientID)
        {
            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                bool isDead = data.lifeCount <= 1;
                if(isDead)
                    playerPanel.SetDead(index);

                if(IsHost)
                {
                    data.lifeCount--;

                    if(isDead)
                    {
                        data.score = GetScore();
                        deadPlayerCount++;
                        Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Dead Player Count : {deadPlayerCount}");
                    }
                    else
                        Respawn(clientID);

                    minigame.PlayerDatas[index] = data;
                    if ((minigame.PlayerDatas.Count - deadPlayerCount) <= 1)
                            FinishCycle();
                }
            });
        }

        public virtual void FinishCycle()
        {
            minigame.PlayerDatas.ChangeData(i => i.IsDead == false, data => {
                data.score = scoreWeight[deadPlayerCount];
                Debug.Log($"Standing Player : {data.clientID} / score : {data.score}");
                return data;
            });
            minigame.FinishGame();
        }

        public void Respawn(ulong clientID)
        {
            playableMinigame.RespawnPlayer(clientID);
        }

        private int GetScore()
        {
            if(Time.time - lastDecisionTime < 0.25f)
                return lastScore;

            lastDecisionTime = Time.time;
            lastScore = scoreWeight[deadPlayerCount];
            return lastScore;
        }
    }
}
