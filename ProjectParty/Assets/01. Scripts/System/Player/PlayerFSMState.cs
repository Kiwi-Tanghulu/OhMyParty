using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.FSM;
using OMG.Player;

namespace OMG.Player.FSM
{
    public class PlayerFSMState : FSMState
    {
        protected PlayerController player;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            player = brain.GetComponent<PlayerController>();
        }
    }
}