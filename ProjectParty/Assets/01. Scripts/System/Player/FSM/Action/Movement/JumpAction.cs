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

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.Jump(jumpPower);
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
