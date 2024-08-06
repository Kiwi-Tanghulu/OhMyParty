using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class IsMove : PlayerFSMDecision
    {
        private CharacterMovement movement;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override bool MakeDecision()
        {
            result = movement.Movement.MoveDir != Vector3.zero;

            base.MakeDecision();
            
            return result;
        }
    }
}