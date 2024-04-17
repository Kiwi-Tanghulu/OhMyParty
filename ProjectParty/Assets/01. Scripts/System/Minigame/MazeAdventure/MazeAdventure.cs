using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventure : PlayableMinigame
    {
        private DeathmatchCycle deathmatchCycle = null;
        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            for (int i = 0; i < playerDatas.Count; ++i)
                CreatePlayer(i);

            deathmatchCycle = cycle as DeathmatchCycle;

            StartIntro();
        }
    }

}
