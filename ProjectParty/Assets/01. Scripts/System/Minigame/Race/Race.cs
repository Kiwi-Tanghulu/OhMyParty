using OMG.Minigames.RockFestival;
using OMG.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.Extensions;

namespace OMG.Minigames.Race
{
    public class Race : PlayableMinigame
    {
        [SerializeField] private float playTime = 60f;
        private RaceCycle raceCycle;

        [Space]
        [SerializeField] private int maxScore;
        [SerializeField] private int scoreInterval;

        private NetworkList<int> rank;
        public NetworkList<int> Rank => rank;
        private int goalCount;

        public Action OnReadyGame;
        public Action OnStartGame;
        public Action<int> OnPlayerGoal;

        protected override void Awake()
        {
            base.Awake();
            
            rank = new NetworkList<int>();
        }

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            for (int i = 0; i < PlayerDatas.Count; ++i)
            {
                CreatePlayer(i);
            }

            raceCycle = cycle as RaceCycle;
            raceCycle.SetPlayTime(playTime);
            
            goalCount = 0;

            StartIntro();
        }

        public void ReadyGame()
        {
            if (!IsHost)
                return;

            OnReadyGame?.Invoke();
        }

        public override void StartGame()
        {
            base.StartGame();

            if (!IsHost)
                return;

            raceCycle.StartCycle();
            OnStartGame?.Invoke();
        }

        public void GoalPlayer(PlayerController player)
        {
            NetworkObject playerNetworkObj = player.GetComponent<NetworkObject>();

            if (IsGoal(playerNetworkObj))
                return;

            int playerIndex = Players.IndexOf(playerNetworkObj);

            rank.Add(playerIndex);
            
            playerDatas.ChangeData(i => i.clientID == playerNetworkObj.OwnerClientId, data => {
                data.score += maxScore - (scoreInterval * goalCount);
                return data;
            });

            GoalPlayerClientRpc(playerIndex);
        }

        [ClientRpc]
        private void GoalPlayerClientRpc(int playerIndex)
        {
            goalCount++;

            OnPlayerGoal?.Invoke(playerIndex);
        }

        public bool IsGoal(NetworkObject player)
        {
            return rank.Contains(Players.IndexOf(player));
        }
    }
}