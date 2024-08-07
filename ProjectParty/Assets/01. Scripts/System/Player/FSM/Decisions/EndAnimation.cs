using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class EndAnimation : PlayerFSMDecision
    {
        private ExtendedAnimator anim;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            anim = player.GetCharacterComponent<PlayerVisual>().Anim;
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.AnimEvent.OnStartEvent.AddListener(StartAnim);
            anim.AnimEvent.OnEndEvent.AddListener(EndAnim);
        }

        public override void ExitState()
        {
            anim.AnimEvent.OnStartEvent.RemoveListener(StartAnim);
            anim.AnimEvent.OnEndEvent.RemoveListener(EndAnim);
        }

        private void StartAnim()
        {
            result = false;
        }

        private void EndAnim()
        {
            result = true;
        }
    }
}