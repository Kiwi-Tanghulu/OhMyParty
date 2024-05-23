using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class MoveState : PlayerFSMState
    {
        private ExtendedAnimator anim;

        private readonly int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.Animator;
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            anim.SetFloat(moveSpeedAnimHash, 1f, true, 0.1f);
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            anim.SetFloat(moveSpeedAnimHash, 0f, true, 0.1f);
        }
    }
}
