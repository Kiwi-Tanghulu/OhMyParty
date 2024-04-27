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
        private PlayerAnimation anim;
        private int speedHash = Animator.StringToHash("speed");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<PlayerMovement>();
            anim = player.Visual.GetComponent<PlayerAnimation>();
        }

        public override void EnterState()
        {
            base.EnterState();

            input.OnActionEvent += IncreaseRunSpeed;

            movement.SetMoveDir(Vector3.forward);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            currentRunSpeed -= decreaseRunSpeedAmount * Time.deltaTime;

            SetRunSpeed();
        }

        public override void ExitState()
        {
            base.ExitState();

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