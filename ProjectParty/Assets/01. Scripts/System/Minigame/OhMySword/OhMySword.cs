using OMG.Inputs;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySword : PlayableMinigame
    {
        private TimeAttackCycle timeAttackCycle = null;
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

            timeAttackCycle = cycle as TimeAttackCycle;
        }
    }
}
