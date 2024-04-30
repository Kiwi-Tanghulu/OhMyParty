using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.FSM;
using OMG.Input;

namespace OMG.Player.FSM
{
    public class SetMoveDirectionAction : PlayerFSMAction
    {
        [SerializeField] private PlayInputSO input;

        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            input.OnMoveEvent += SetMoveDir;
        }

        public override void ExitState()
        {
            base.ExitState();

            input.OnMoveEvent -= SetMoveDir;
        }

        private void SetMoveDir(Vector2 input)
        {
            Vector3 moveDir = new Vector3(input.x, 0f, input.y);

            movement.SetMoveDir(moveDir);
        }
    }
}
