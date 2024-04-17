using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using OMG.FSM;
using OMG.Input;
using OMG.Player;

namespace OMG.Player.FSM
{
    public abstract class ActionState : PlayerFSMState
    {
        [SerializeField] private PlayInputSO input;

        [Space]
        [SerializeField] private bool applyMove;

        private PlayerMovement movement;
        private PlayerAnimation anim;

        private readonly int actionAnimHash = Animator.StringToHash("action");
        private readonly int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<PlayerMovement>();
            anim = player.transform.Find("Visual").GetComponent<PlayerAnimation>();
        }

        public override void EnterState()
        {
            base.EnterState();

            if(!applyMove)
                movement.SetMoveDir(Vector3.zero);
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            if (applyMove)
            {
                input.OnMoveEvent += SetMoveDir;
                input.OnMoveEvent += SetMoveAnim;
            }

            anim.OnPlayingEvent += DoAction;
            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
            anim.SetTrigger(actionAnimHash);
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();

            if (applyMove)
                movement.Move();
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            if (applyMove)
            {
                input.OnMoveEvent -= SetMoveDir;
                input.OnMoveEvent -= SetMoveAnim;
            }

            anim.OnPlayingEvent -= DoAction;
            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);

            Debug.Log("exit action state");
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
