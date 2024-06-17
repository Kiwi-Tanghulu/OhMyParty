using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using OMG.Client.Component;
using OMG.FSM;
using OMG.Player;
using OMG.Ragdoll;

namespace OMG.Player.FSM
{
    public class RecoveryState : PlayerFSMState
    {
        private ExtendedAnimator anim;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.Animator;
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnEndEvent += AnimEvent_OnEndEvent;
        }

        public override void ExitState()
        {
            anim.AnimEvent.OnEndEvent -= AnimEvent_OnEndEvent;
        }

        private void AnimEvent_OnEndEvent()
        {
            ChangeDefaultState();
        }

        private void ChangeDefaultState()
        {
            if(player.IsServer)
            {
                brain.ChangeState(brain.DefaultState);
            }
        }
    }
}