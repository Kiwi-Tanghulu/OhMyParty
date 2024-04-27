using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class GravityAction : PlayerFSMAction
    {
        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.Rigidbody.useGravity = true;
        }

        public override void ExitState()
        {
            base.ExitState();

            movement.Rigidbody.useGravity = false;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Gravity();
        }
    }
}