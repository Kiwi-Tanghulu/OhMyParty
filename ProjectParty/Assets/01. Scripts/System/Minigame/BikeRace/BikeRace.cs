using OMG.Minigames.RockFestival;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.BikeRace
{
    public class BikeRace : PlayableMinigame
    {
        [SerializeField] private float playTime = 60f;
        private BikeRaceCycle bikeRaceCycle;

        public Action<int> OnPlayerGoal;
        private bool[] isGoal;

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
        }

        public void PlayerGoal(int index)
        {
            isGoal[index] = true;
            OnPlayerGoal?.Invoke(index);
        }
    }
}