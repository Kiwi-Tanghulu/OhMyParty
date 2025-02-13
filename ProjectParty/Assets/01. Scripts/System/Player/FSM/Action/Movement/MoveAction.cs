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

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            player.GetCharacterComponent<PlayerVisual>().Anim.AnimEvent.
                OnPlayingSubEvent.AddListener(AnimEvent_OnPlayingSubEvent);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.Move();
        }

        public override void ExitState()
        {
            base.ExitState();

            player.GetCharacterComponent<PlayerVisual>().Anim.AnimEvent.
                OnPlayingSubEvent.RemoveListener(AnimEvent_OnPlayingSubEvent);
        }

        private void AnimEvent_OnPlayingSubEvent()
        {
            FootstepEvent.Invoke();
        }
    }
}