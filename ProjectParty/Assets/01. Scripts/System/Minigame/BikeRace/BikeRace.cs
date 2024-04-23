using OMG.Minigames.RockFestival;
using OMG.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.Extensions;

namespace OMG.Minigames.BikeRace
{
    public class BikeRace : PlayableMinigame
    {
        [SerializeField] private float playTime = 60f;
        private BikeRaceCycle bikeRaceCycle;

        [Space]
        [SerializeField] private int maxScore;
        [SerializeField] private int scoreInterval;

        private bool[] isGoal;
        private int goalCount;

        public Action OnStartGame;
        public Action<int> OnPlayerGoal;

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            for (int i = 0; i < PlayerDatas.Count; ++i)
            {
                CreatePlayer(i);
            }

            bikeRaceCycle = cycle as BikeRaceCycle;

            bikeRaceCycle.Init(this);
            bikeRaceCycle.SetPlayTime(playTime);

            isGoal = new bool[PlayerDatas.Count];
            goalCount = 0;

            StartIntro();
        }

        public override void StartGame()
        {
            base.StartGame();

            if (IsHost == false)
                return;

            bikeRaceCycle.StartCycle();
            OnStartGame?.Invoke();
        }

        public void GoalPlayer(PlayerController player)
        {
            NetworkObject playerNetworkObj = player.GetComponent<NetworkObject>();
            int playerIndex = Players.IndexOf(playerNetworkObj);

            isGoal[playerIndex] = true;
            playerDatas.ChangeData(i => i.clientID == playerNetworkObj.OwnerClientId, data => {
                data.score += maxScore - (scoreInterval * goalCount);
                return data;
            });
            goalCount++;

            OnPlayerGoal?.Invoke(playerIndex);
        }
    }
}