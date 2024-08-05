using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class ObstacleRun : PlayableMinigame
    {
        private RaceCycle raceCycle;

        public override void Init()
        {
            base.Init();

            raceCycle = cycle as RaceCycle;
        }
    }
}