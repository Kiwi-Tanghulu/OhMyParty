using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class StunState : FSMState
    {
        private PlayerRagdoll ragdoll;

        private int stunAnimHash = Animator.StringToHash("stun");

        public override void InitState(ActioningPlayer actioningPlayer)
        {
            base.InitState(actioningPlayer);

            ragdoll = actioningPlayer.transform.Find("Visual").GetComponent<PlayerRagdoll>();
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0);
            anim.SetBool(stunAnimHash, true);

            ragdoll.SetActive(true);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.SetBool(stunAnimHash, false);

            ragdoll.SetActive(false);
        }
    }
}