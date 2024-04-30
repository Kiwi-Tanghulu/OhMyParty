using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class SetFloatAnimationAction : PlayerAnimationFSMAction
    {
        [Space]
        [SerializeField] private bool setOnEnter;
        [SerializeField] private float onEnterValue;
        [SerializeField] private bool onEnterLerping;
        [SerializeField] private float onEnterLerpingTime;

        [Space]
        [SerializeField] private bool setOnExit;
        [SerializeField] private float onExitValue;
        [SerializeField] private bool onExitLerping;
        [SerializeField] private float onExitLerpingTime;

        public override void EnterState()
        {
            base.EnterState();

            if (setOnEnter)
                anim.SetFloat(hash, onEnterValue, onEnterLerping, onEnterLerpingTime);
        }

        public override void ExitState()
        {
            base.ExitState();

            if (setOnExit)
                anim.SetFloat(hash, onExitValue, onExitLerping, onExitLerpingTime);
        }
    }
}