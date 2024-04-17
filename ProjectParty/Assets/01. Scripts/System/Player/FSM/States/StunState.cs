using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class StunState : PlayerFSMState
    {
        private PlayerRagdoll ragdoll;
        private PlayerHealth health;
        private PlayerMovement movement;

        private int stunAnimHash = Animator.StringToHash("stun");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            health = player.GetComponent<PlayerHealth>();
            ragdoll = player.transform.Find("Visual").GetComponent<PlayerRagdoll>();
        }

        public override void EnterState()
        {
            base.EnterState();

            ragdoll.SetActive(true);
            ragdoll.AddForceAtPosition(health.Damage * health.HitDir, health.HitPoint, ForceMode.Impulse);

            movement.ApplyGravity = false;
        }

        public override void ExitState()
        {
            base.ExitState();

            ragdoll.SetActive(false);
        }
    }
}