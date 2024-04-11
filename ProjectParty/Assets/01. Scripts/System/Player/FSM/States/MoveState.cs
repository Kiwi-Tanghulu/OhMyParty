using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    public class MoveState : FSMState
    {
        [SerializeField] private PlayInputSO input;

        private int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            input.OnMoveEvent += SetMoveDir;

            anim.SetFloat(moveSpeedAnimHash, 1f, true, 0.1f);
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();

            movement.Move();
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            input.OnMoveEvent -= SetMoveDir;
        }

        private void SetMoveDir(Vector2 input)
        {
            Vector3 moveDir = new Vector3(input.x, 0f, input.y);

            movement.SetMoveDir(moveDir);
        }
    }
}
