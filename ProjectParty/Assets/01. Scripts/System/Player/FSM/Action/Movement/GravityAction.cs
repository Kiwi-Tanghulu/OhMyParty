using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class GravityAction : PlayerFSMAction
    {
        private CharacterMovement movement;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Gravity();
        }
    }
}