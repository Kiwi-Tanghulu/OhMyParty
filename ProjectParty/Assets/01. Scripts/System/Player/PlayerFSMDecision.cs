using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class PlayerFSMDecision : FSMDecision
    {
        protected PlayerController player;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            player = brain.GetComponent<PlayerController>();
        }
    }
}
