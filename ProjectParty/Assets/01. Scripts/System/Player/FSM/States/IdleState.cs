using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class IdleState : FSMState
    {
        [SerializeField] private PlayInputSO input;

        private int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void EnterState()
        {
            base.EnterState();

            input.OnMoveEvent += SetMoveDir;
            movement.SetMoveDir(Vector3.zero);

            anim.SetFloat(moveSpeedAnimHash, 0f, true, 0.1f);
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