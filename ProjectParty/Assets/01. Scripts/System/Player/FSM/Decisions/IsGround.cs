using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class IsGround : PlayerFSMDecision
    {
        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<PlayerMovement>();
        }

        public override bool MakeDecision()
        {
            result = movement.IsGround;

            return base.MakeDecision();
        }
    }
}