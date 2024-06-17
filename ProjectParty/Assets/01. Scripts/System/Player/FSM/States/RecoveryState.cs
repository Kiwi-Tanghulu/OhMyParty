using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using OMG.Client.Component;
using OMG.FSM;
using OMG.Player;
using OMG.Ragdoll;
using OMG.Extensions;

namespace OMG.Player.FSM
{
    public class RecoveryState : PlayerFSMState
    {
        private ExtendedAnimator anim;
        private PlayerHealth health;

        [SerializeField] private float playerHitableDelayTime = 1f;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.Animator;
            health = player.GetComponent<PlayerHealth>();
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnEndEvent += AnimEvent_OnEndEvent;

            health.Hitable = false;
            health.PlayerHitable = false;
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnEndEvent -= AnimEvent_OnEndEvent;

            health.Hitable = true;
            this.DelayCoroutine(playerHitableDelayTime, () => health.PlayerHitable = true);
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