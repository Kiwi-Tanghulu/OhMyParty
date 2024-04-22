using OMG.Minigames.RockFestival;
using OMG.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.BikeRace
{
    public class BikeRace : PlayableMinigame
    {
        [SerializeField] private float playTime = 60f;
        private BikeRaceCycle bikeRaceCycle;

        private bool[] isGoal;

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
            int index = Players.IndexOf(player.GetComponent<NetworkObject>());

            Debug.Log($"Goal Player : {index}");
            isGoal[index] = true;
            OnPlayerGoal?.Invoke(index);
        }
    }
}