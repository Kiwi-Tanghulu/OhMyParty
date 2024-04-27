using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class SetMoveSpeedAction : PlayerFSMAction
    {
        [SerializeField] private float moveSpeed;

        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.SetMoveSpeed(moveSpeed);
        }
    }
}