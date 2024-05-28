using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class AccelationMoveAction : MoveAction
    {
        [SerializeField] private float maxMoveSpeed;
        [SerializeField] private float accelation;

        public override void UpdateState()
        {
            base.UpdateState();

            float speed = movement.MaxMoveSpeed;
            speed += Time.deltaTime * accelation;
            speed = Mathf.Min(speed, maxMoveSpeed);
            movement.SetMoveSpeed(speed);
        }
    }
}