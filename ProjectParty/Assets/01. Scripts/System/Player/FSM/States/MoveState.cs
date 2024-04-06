using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class MoveState : FSMState
    {
        [SerializeField] private PlayInputSO input;

        private int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void EnterState()
        {
            base.EnterState();

            input.OnMoveEvent += SetMoveDir;

            anim.SetFloat(moveSpeedAnimHash, 1f, true, 0.1f);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Move();
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
