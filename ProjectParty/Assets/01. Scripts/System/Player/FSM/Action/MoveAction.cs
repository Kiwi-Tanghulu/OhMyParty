using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.Input;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class MoveAction : PlayerFSMAction
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

            movement.Move();
        }
    }
}