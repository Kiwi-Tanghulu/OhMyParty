using OMG.FSM;
using OMG.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    [RequireComponent(typeof(GravityAction))]
    public class RisingJumpAction : PlayerFSMAction
    {
        [SerializeField] private PlayInputSO input;

        [Space]
        [SerializeField] private float minHeight;
        [SerializeField] private float maxHeight;

        private CharacterMovement movement;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.Movement.StartRisingJump(minHeight, maxHeight);

            input.OnJumpEvent += (started) =>
            {
                if (started == false)
                    movement.Movement.StopRisingJump();
            };
        }

        public override void ExitState()
        {
            base.ExitState();

            movement.Movement.StopRisingJumpImmediately();
        }
    }
}