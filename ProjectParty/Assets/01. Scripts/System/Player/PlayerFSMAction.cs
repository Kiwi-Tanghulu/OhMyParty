using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.FSM;
using OMG.Player;

namespace OMG.Player.FSM
{
    public class PlayerFSMAction : FSMAction
    {
        protected PlayerController player;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            player = brain.GetComponent<PlayerController>();
        }
    }
}