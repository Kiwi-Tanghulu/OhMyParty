using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace OMG.Player
{
    public abstract class ActionState : FSMState
    {
        [SerializeField] private PlayInputSO input;

        [Space]
        [SerializeField] private bool applyMove;

        private int actionAnimHash = Animator.StringToHash("action");
        private int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void EnterState()
        {
            base.EnterState();

            if(applyMove)
            {
                input.OnMoveEvent += SetMoveDir;
                input.OnMoveEvent += SetMoveAnim;
            }
            else
            {
                movement.SetMoveDir(Vector3.zero);
            }

            anim.OnPlayingEvent += DoAction;
            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
            anim.SetTrigger(actionAnimHash);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (applyMove)
                movement.Move();
        }

        public override void ExitState()
        {
            base.ExitState();

            if (applyMove)
            {
                input.OnMoveEvent -= SetMoveDir;
                input.OnMoveEvent -= SetMoveAnim;
            }

            anim.OnPlayingEvent -= DoAction;
            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }

        protected abstract void DoAction();

        private void SetMoveDir(Vector2 input)
        {
            Vector3 moveDir = new Vector3(input.x, 0f, input.y);

            movement.SetMoveDir(moveDir);
        }

        private void SetMoveAnim(Vector2 input)
        {
            if (input == Vector2.zero)
                anim.SetFloat(moveSpeedAnimHash, 0, true, 0.1f);
            else
                anim.SetFloat(moveSpeedAnimHash, 1, true, 0.1f);
        }
    }
}
