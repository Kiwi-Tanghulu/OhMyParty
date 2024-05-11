using OMG.FSM;
using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class ObstacleRunRunState : PlayerFSMState
    {
        [SerializeField] private PlayInputSO input;

        [Space]
        [SerializeField] private float minRunSpeed;
        [SerializeField] private float maxRunSpeed;
        [SerializeField] private float increaseRunSpeedAmount;
        [SerializeField] private float decreaseRunSpeedAmount;
        private float currentRunSpeed;

        private PlayerMovement movement;
        private ExtendedAnimator anim;
        private int speedHash = Animator.StringToHash("speed");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<PlayerMovement>();
            anim = player.Visual.GetComponent<ExtendedAnimator>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            input.OnActionEvent += IncreaseRunSpeed;

            if (movement.MoveSpeed <= 0f)
            {
                currentRunSpeed = minRunSpeed;
                SetRunSpeed();
            }

            movement.SetMoveDir(Vector3.forward);
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();

            currentRunSpeed -= decreaseRunSpeedAmount * Time.deltaTime;

            SetRunSpeed();
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            input.OnActionEvent -= IncreaseRunSpeed;
        }

        private void IncreaseRunSpeed()
        {
            currentRunSpeed += increaseRunSpeedAmount;
        }

        private void SetRunSpeed()
        {
            currentRunSpeed = Mathf.Clamp(currentRunSpeed, minRunSpeed, maxRunSpeed);

            movement.SetMoveSpeed(currentRunSpeed);
            anim.SetFloat(speedHash, currentRunSpeed / maxRunSpeed);
        }
    }
}