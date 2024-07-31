using OMG.FSM;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class MoveState : PlayerFSMState
    {
        private ExtendedAnimator anim;

        private readonly int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            anim = player.GetCharacterComponent<PlayerVisual>().Anim;
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.SetFloat(moveSpeedAnimHash, 1f, true, 0.1f);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.SetFloat(moveSpeedAnimHash, 0f, true, 0.1f);
        }
    }
}
