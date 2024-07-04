using OMG.Inputs;
using UnityEngine;

namespace OMG.Minigames.PunchClub
{
    public class PunchClub : PlayableMinigame
    {
        private DeathmatchCycle deathmatchCycle = null;
        protected override bool ShufflePosition => true;

        #region Test

        protected override void Awake()
        {
            base.Awake();
            InputManager.ChangeInputMap(InputMapType.Play);
        }

        #endregion

        public override void Init()
        {
            base.Init();

            if(IsHost == false)
                return;

            deathmatchCycle = cycle as DeathmatchCycle;
        }
    }
}
