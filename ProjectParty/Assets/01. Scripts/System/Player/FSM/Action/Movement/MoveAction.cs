using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.Inputs;
using OMG.FSM;
using UnityEngine.Events;

namespace OMG.Player.FSM
{
    public class MoveAction : PlayerFSMAction
    {
        public UnityEvent FootstepEvent;

        protected CharacterMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            player.Animator.AnimEvent.OnPlayingSubEvent += AnimEvent_OnPlayingSubEvent;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Move();
        }

        public override void ExitState()
        {
            base.ExitState();

            player.Animator.AnimEvent.OnPlayingSubEvent -= AnimEvent_OnPlayingSubEvent;
        }

        private void AnimEvent_OnPlayingSubEvent()
        {
            FootstepEvent.Invoke();
        }
    }
}