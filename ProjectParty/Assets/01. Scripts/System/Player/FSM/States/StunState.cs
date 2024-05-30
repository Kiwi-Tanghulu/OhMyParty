using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Player;
using OMG.FSM;
using OMG.Ragdoll;

namespace OMG.Player.FSM
{
    public class StunState : PlayerFSMState
    {
        private RagdollController ragdoll;
        private PlayerHealth health;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            health = player.GetComponent<PlayerHealth>();
            ragdoll = player.Visual.Ragdoll;
        }

        public override void EnterState()
        {
            base.EnterState();

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