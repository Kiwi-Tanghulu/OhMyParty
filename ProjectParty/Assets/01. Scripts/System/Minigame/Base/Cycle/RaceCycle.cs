using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.UI.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class RaceCycle : MinigameCycle
    {
        [SerializeField] int[] scoreWeight = { 1000, 500, 100, 50 };

        [Space(15f)]
        [SerializeField] NetworkEvent<UlongParams> onPlayerGolaEvent = new NetworkEvent<UlongParams>("PlayerGoal");
        private DeathmatchPlayerPanel playerPanel = null;
        private PlayableMinigame playableMinigame = null;

        private int goalPlayerCount = 0;
        public int GoalPlayerCount => goalPlayerCount;

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

            onPlayerGolaEvent.AddListener(HandlePlayerGoal);
            onPlayerGolaEvent.Register(NetworkObject);
        }

        public virtual void SetPlayerGoal(ulong clientID)
        {
            onPlayerGolaEvent?.Broadcast(clientID);
        }

        private void HandlePlayerGoal(UlongParams clientID)
        {
            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                bool isDead = data.lifeCount <= 1;
                if (isDead)
                    playerPanel.SetDead(index);

                if (IsHost)
                {
                    data.lifeCount--;

                    if (isDead)
                    {
                        data.score = GetScore();
                        goalPlayerCount++;
                        Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Dead Player Count : {goalPlayerCount}");
                    }

                    minigame.PlayerDatas[index] = data;
                    if ((minigame.PlayerDatas.Count - goalPlayerCount) <= 1)
                        FinishCycle();
                }
            });
        }

        public virtual void FinishCycle()
        {
            minigame.PlayerDatas.ChangeData(i => i.IsDead == false, data => {
                data.score = scoreWeight[goalPlayerCount];
                Debug.Log($"Standing Player : {data.clientID} / score : {data.score}");
                return data;
            });
            minigame.FinishGame();
        }

        private int GetScore()
        {
            if (Time.time - lastDecisionTime < 0.25f)
                return lastScore;

            lastDecisionTime = Time.time;
            lastScore = scoreWeight[goalPlayerCount];
            return lastScore;
        }
    }
}