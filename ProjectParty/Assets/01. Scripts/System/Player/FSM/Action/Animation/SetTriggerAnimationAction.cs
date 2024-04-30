using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class SetTriggerAnimationAction : PlayerAnimationFSMAction
    {
        public override void EnterState()
        {
            base.EnterState();

            anim.SetTrigger(hash);
        }
    }
}