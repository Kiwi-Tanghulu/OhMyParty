using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class StunState : FSMState
    {
        private PlayerRagdoll ragdoll;
        private PlayerHealth health;

        private int stunAnimHash = Animator.StringToHash("stun");

        public override void InitState(ActioningPlayer actioningPlayer)
        {
            base.InitState(actioningPlayer);

            health = actioningPlayer.GetComponent<PlayerHealth>();
            ragdoll = actioningPlayer.transform.Find("Visual").GetComponent<PlayerRagdoll>();
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0);

            ragdoll.SetActive(true);
            ragdoll.AddForce(health.Damage * health.HitDir, ForceMode.Impulse);
        }

        public override void ExitState()
        {
            base.ExitState();

            ragdoll.SetActive(false);
        }
    }
}