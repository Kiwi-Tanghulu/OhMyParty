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
        [SerializeField] protected int[] scoreWeight = { 1000, 500, 100, 50 };

        [Space(15f)]
        [SerializeField] NetworkEvent<UlongParams> onPlayerGolaEvent = new NetworkEvent<UlongParams>("PlayerGoal");
        private RacePlayerPanel playerPanel = null;
        private PlayableMinigame playableMinigame = null;

        protected int goalPlayerCount = 0;
        public int GoalPlayerCount => goalPlayerCount;

        protected float lastDecisionTime = 0f;
        protected int lastScore = 0;

        protected override void Awake()
        {
            base.Awake();
            playerPanel = minigame.MinigamePanel.PlayerPanel as RacePlayerPanel;
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
            if (!IsHost)
                return;

            onPlayerGolaEvent?.Broadcast(clientID);
        }

        protected virtual void HandlePlayerGoal(UlongParams clientID)
        {
            Debug.Log($"Goal Player : {clientID.Value}");

            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                playerPanel.SetGoal(index);

                if (IsHost)
                {
                    data.lifeCount--;

                    data.score = GetScore();
                    goalPlayerCount++;
                    Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Goal Player Count : {goalPlayerCount}");

                    minigame.PlayerDatas[index] = data;
                    if ((minigame.PlayerDatas.Count - goalPlayerCount) <= 1)
                        FinishCycle();
                }
            });
        }

        public virtual void FinishCycle()
        {
            //minigame.PlayerDatas.ChangeData(i => i.IsDead == false, data => {
            //    data.score = scoreWeight[goalPlayerCount];
            //    Debug.Log($"Standing Player : {data.clientID} / score : {data.score}");
            //    return data;
            //});
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