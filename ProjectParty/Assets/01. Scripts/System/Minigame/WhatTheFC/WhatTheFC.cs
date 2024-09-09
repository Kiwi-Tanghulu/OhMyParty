using UnityEngine;

namespace OMG.Minigames.WhatTheFC
{
    public class WhatTheFC : PlayableMinigame
    {
        protected override void OnGameInit()
        {
            base.OnGameInit();
            (cycle as TeamMinigameCycle).DecideTeam();
        }
    }
}
