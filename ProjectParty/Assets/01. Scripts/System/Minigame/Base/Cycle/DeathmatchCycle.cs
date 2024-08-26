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
        [SerializeField] float decisionThreshold = 0.25f;
        private float lastDecisionTime = 0f;
        private int lastScore = 0;

        [Space(15f)]
        [SerializeField] NetworkEvent<UlongParams> onPlayerDeadEvent = new NetworkEvent<UlongParams>("PlayerDead");
        private DeathmatchPlayerPanel playerPanel = null;
        private PlayableMinigame playableMinigame = null;

        private int deadPlayerCount = 0;
        public int DeadPlayerCount => deadPlayerCount;

        public override void Init(Minigame minigame)
        {
            base.Init(minigame);

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
                {
                    playerPanel.SetDead(index, true);
                    playerPanel.SetDead(index, 0);
                    OnPlayerDead(clientID);
                }
                else
                {
                    playerPanel.SetDead(index, playableMinigame.LifeCount - data.lifeCount + 1);
                }

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
                    int leftPlayerCount = minigame.PlayerDatas.Count - deadPlayerCount;
                    if (deadPlayerCount > 0 && leftPlayerCount <= 1)
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

        protected virtual void OnPlayerDead(ulong clientID)
        {

        }

        private int GetScore()
        {
            if(Time.time - lastDecisionTime < decisionThreshold)
                return lastScore;

            lastDecisionTime = Time.time;
            lastScore = scoreWeight[deadPlayerCount];
            return lastScore;
        }
    }
}
