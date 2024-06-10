using OMG.FSM;
using OMG.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    [RequireComponent(typeof(GravityAction))]
    public class JumpAction : PlayerFSMAction
    {
        private CharacterMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.Jump();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
