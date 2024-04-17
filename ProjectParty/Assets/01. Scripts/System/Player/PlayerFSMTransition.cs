using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class PlayerFSMTransition : FSMTransition
    {
        protected PlayerController player;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            player = brain.GetComponent<PlayerController>();
        }
    }
}