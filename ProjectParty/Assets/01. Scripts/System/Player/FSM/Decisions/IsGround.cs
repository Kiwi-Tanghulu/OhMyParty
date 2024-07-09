using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class IsGround : PlayerFSMDecision
    {
        private CharacterMovement movement;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override bool MakeDecision()
        {
            result = movement.Movement.IsGround;

            return base.MakeDecision();
        }
    }
}