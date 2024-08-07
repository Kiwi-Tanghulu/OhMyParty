using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using OMG.NetworkEvents;
using OMG.Player;
using OMG.UI.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class RaceCycle : MinigameCycle
    {
        [SerializeField] protected int[] scoreWeight = { 1000, 500, 100, 50 };

        [Space(15f)]
        NetworkEvent<UlongParams> onPlayerGoalRpc = new NetworkEvent<UlongParams>("PlayerGoal");
        [SerializeField] UnityEvent<Transform> OnPlayerGoalEvent;
        private RacePlayerPanel playerPanel = null;
        private PlayableMinigame playableMinigame = null;

        protected int goalPlayerCount = 0;
        public int GoalPlayerCount => goalPlayerCount;

        protected float lastDecisionTime = 0f;
        protected int lastScore = 0;

        private void Start()
        {
            playerPanel = minigame.MinigamePanel.PlayerPanel as RacePlayerPanel;
            playableMinigame = minigame as PlayableMinigame;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn(); 

            onPlayerGoalRpc.AddListener(HandlePlayerGoal);
            onPlayerGoalRpc.Register(NetworkObject);
        }

        public virtual void SetPlayerGoal(ulong clientID)
        {
            if (!IsHost)
                return;

            onPlayerGoalRpc?.Broadcast(clientID);
        }

        protected virtual void HandlePlayerGoal(UlongParams clientID)
        {
            Debug.Log($"Goal Player : {clientID.Value}");
            PlayerController player = playableMinigame.PlayerDictionary[clientID];

            minigame.PlayerDatas.ForEach((data, index) => {
                if (data.clientID != clientID)
                    return;

                playerPanel.SetGoal(index);
                OnPlayerGoalEvent?.Invoke(player.FeedbackPlayPoint);

                if (IsHost)
                {
                    data.lifeCount--;

                    data.score = GetScore();
                    goalPlayerCount++;
                    Debug.Log($"Player Count : {minigame.PlayerDatas.Count} / Goal Player Count : {goalPlayerCount}");

                    StartCoroutine(this.DelayCoroutine(2f, () =>
                    {
                        player.NetworkObject.Despawn(true);
                    }));

                    minigame.PlayerDatas[index] = data;
                    if (minigame.PlayerDatas.Count == GoalPlayerCount)
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