using OMG.FSM;
using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class JumpAction : FSMAction
    {
        [SerializeField] private PlayInputSO inputSO;

        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();
            
            inputSO.OnJumpEvent += movement.Jump;
        }

        public override void ExitState()
        {
            base.ExitState();

            inputSO.OnJumpEvent -= movement.Jump;
        }
    }
}
