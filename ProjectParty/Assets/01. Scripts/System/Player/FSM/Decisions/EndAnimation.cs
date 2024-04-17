using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class EndAnimation : PlayerFSMDecision
    {
        private PlayerAnimation anim;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            anim = player.transform.Find("Visual").GetComponent<PlayerAnimation>();
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.OnStartEvent += StartAnim;
            anim.OnEndEvent += EndAnim;
        }

        public override void ExitState()
        {
            anim.OnStartEvent -= StartAnim;
            anim.OnEndEvent -= EndAnim;
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