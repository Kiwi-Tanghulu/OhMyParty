using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class AccelationMoveAction : FSMAction
    {
        [SerializeField] private float maxMoveSpeed;
        [SerializeField] private float accelation;

        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<PlayerMovement>();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            float speed = movement.MoveSpeed;
            speed += Time.deltaTime * accelation;
            speed = Mathf.Min(speed, maxMoveSpeed);
            movement.SetMoveSpeed(speed);
            movement.Move();
        }
    }
}