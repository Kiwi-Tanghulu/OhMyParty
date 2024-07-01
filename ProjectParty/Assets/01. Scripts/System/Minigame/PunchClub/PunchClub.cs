using UnityEngine;

namespace OMG.Minigames.PunchClub
{
    public class PunchClub : PlayableMinigame
    {
        private DeathmatchCycle deathmatchCycle = null;
        protected override bool ShufflePosition => true;

        public override void Init()
        {
            base.Init();

            if(IsHost == false)
                return;

            deathmatchCycle = cycle as DeathmatchCycle;
        }
    }
}
