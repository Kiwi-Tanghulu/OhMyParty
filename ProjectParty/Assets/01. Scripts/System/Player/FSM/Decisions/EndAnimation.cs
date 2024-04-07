using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class EndAnimation : FSMDecision
    {
        private PlayerAnimation anim;

        public override void Init(ActioningPlayer actioningPlayer)
        {
            base.Init(actioningPlayer);

            anim = actioningPlayer.transform.Find("Visual").GetComponent<PlayerAnimation>();
        }

        public override void EnterState()
        {
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