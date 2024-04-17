using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.BikeRace
{
    public class BikeRaceCycle : TimeAttackCycle
    {
        private int goalCount;

        public void Init(BikeRace bikeRace)
        {
            goalCount = 0;
            
            bikeRace.OnPlayerGoal += OnPlayerGoal;
        }

        private void OnPlayerGoal(int index)
        {
            goalCount++;

            if (goalCount >= minigame.PlayerDatas.Count)
                FinishCycle();
        }
    }
}
