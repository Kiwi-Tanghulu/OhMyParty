using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class SetBooleanAnimationAction : PlayerAnimationFSMAction
    {
        public bool setOnEnter;
        public bool setOnExit;

        [SerializeField] private bool onEnterValue;
        [SerializeField] private bool onExitValue;

        public override void EnterState()
        {
            base.EnterState();

            if (setOnEnter)
                anim.SetBool(hash, onEnterValue);
        }

        public override void ExitState()
        {
            base.ExitState();

            if (setOnExit)
                anim.SetBool(hash, onExitValue);
        }
    }
}