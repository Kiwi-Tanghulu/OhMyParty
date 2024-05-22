using OMG.FSM;
using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    [RequireComponent(typeof(GravityAction))]
    public class JumpAction : PlayerFSMAction
    {
        [SerializeField] private float jumpPower;

        private CharacterMovement movement;
        private ExtendedAnimator anim;

        private int isGroundHash = Animator.StringToHash("isGround");

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<CharacterMovement>();
            anim = player.Animator;
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.Jump(jumpPower);
            anim.SetBool(isGroundHash, false);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.SetBool(isGroundHash, true);
        }
    }
}
