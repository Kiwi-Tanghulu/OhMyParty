using OMG.FSM;
using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class JumpAction : FSMAction
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

            Jump();
        }

        private void Jump()
        {
            movement.Jump(jumpPower);
        }
    }
}
