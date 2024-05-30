using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.Inputs;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class MoveAction : PlayerFSMAction
    {
        protected CharacterMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Move();
        }
    }
}