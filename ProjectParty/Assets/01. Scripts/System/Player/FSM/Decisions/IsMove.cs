using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class IsMove : FSMDecision
    {
        private PlayerMovement movement;

        public override void Init(ActioningPlayer actioningPlayer)
        {
            base.Init(actioningPlayer);

            movement = actioningPlayer.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {

        }

        public override bool MakeDecision()
        {
            result = movement.MoveDir != Vector3.zero;

            base.MakeDecision();

            return result;
        }

        public override void ExitState()
        {

        }
    }
}