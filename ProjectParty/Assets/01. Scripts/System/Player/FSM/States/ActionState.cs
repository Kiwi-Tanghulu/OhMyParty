using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using OMG.FSM;
using OMG.Inputs;
using OMG.Player;
using Unity.XR.OpenVR;

namespace OMG.Player.FSM
{
    public abstract class ActionState : PlayerFSMState
    {
        private ExtendedAnimator anim;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.transform.Find("Visual").GetComponent<ExtendedAnimator>();
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnPlayingEvent += DoAction;
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            
            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnPlayingEvent -= DoAction;
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }

        protected abstract void DoAction();
    }
}
