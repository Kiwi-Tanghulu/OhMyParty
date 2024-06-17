using OMG.FSM;
using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class DieState : PlayerFSMState
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
            ragdoll.AddForce(0f * health.HitDir, ForceMode.Impulse);
        }
    }
}
