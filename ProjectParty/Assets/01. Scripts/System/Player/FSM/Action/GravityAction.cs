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

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Gravity();
        }
    }
}