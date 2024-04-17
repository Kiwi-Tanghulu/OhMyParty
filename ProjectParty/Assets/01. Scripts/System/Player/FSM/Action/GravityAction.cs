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

            movement.ApplyGravity = true;
        }

        public override void ExitState()
        {
            base.ExitState();

            movement.ApplyGravity = false;
        }
    }
}