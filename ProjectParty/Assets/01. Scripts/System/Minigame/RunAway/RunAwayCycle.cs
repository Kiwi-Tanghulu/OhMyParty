using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.UI.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class RunAwayCycle : RaceCycle
    {
        [Space(15f)]
        [SerializeField] NetworkEvent<UlongParams> onPlayerDeadEvent = new NetworkEvent<UlongParams>("PlayerDead");

        private int deadPlayerCount = 0;
        public int DeadPlayerCount => deadPlayerCount;

        public int FinishPlayerCount => deadPlayerCount + GoalPlayerCount;

        private RunAwayPlayerPanel playerPanel = null;


        public override void Init(Minigame minigame)
        {
            base.Init(minigame);

            playerPanel = minigame.MinigamePanel.PlayerPanel as RunAwayPlayerPanel;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onPlayerDeadEvent.AddListener(HandlePlayerDead);
            onPlayerDeadEvent.Register(NetworkObject);
        }

        public virtual void SetPlayerDead(ulong clientID)
        {
            if (!IsHost)
                return;

            onPlayerDeadEvent?.Broadcast(clientID);
        }

        private void HandlePlayerDead(UlongParams clientID)
        {
            Debug.Log($"Dead Player : {clientID.Value}");

            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                playerPanel.SetDead(index);

                if (IsHost)
                {
                    deadPlayerCount++;
                    data.score = GetScore(scoreWeight.Length - deadPlayerCount);
                    Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Dead Player Count : {deadPlayerCount}");

                    minigame.PlayerDatas[index] = data;
                    if (minigame.PlayerDatas.Count == FinishPlayerCount)
                        FinishCycle();
                }
            });
        }

        protected override void HandlePlayerGoal(UlongParams clientID)
        {
            Debug.Log($"Goal Player : {clientID.Value}");

            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                playerPanel.SetGoal(index);

                if (IsHost)
                {
                    data.score = GetScore(goalPlayerCount);
                    goalPlayerCount++;
                    Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Goal Player Count : {goalPlayerCount}");

                    minigame.PlayerDatas[index] = data;
                    if (minigame.PlayerDatas.Count == FinishPlayerCount)
                        FinishCycle();
                }
            });
        }

        private int GetScore(int rank)
        {
            if (Time.time - lastDecisionTime < 0.25f)
                return lastScore;

            lastDecisionTime = Time.time;
            lastScore = scoreWeight[rank];
            return lastScore;
        }
    }
}
