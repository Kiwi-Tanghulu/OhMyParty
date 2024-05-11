using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using OMG.FSM;
using OMG.Input;
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

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            
            anim.OnPlayingEvent += DoAction;
            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            anim.OnPlayingEvent -= DoAction;
            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }

        protected abstract void DoAction();
    }
}
