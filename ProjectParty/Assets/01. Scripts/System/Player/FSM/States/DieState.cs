using OMG.FSM;
using OMG.NetworkEvents;
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

        private NetworkEvent playerDieEvent = new NetworkEvent("PlayerDieEvent");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            health = player.GetComponent<PlayerHealth>();
            ragdoll = player.Visual.Ragdoll;
        }

        public override void NetworkInit()
        {
            base.NetworkInit();

            playerDieEvent.AddListener(Die);
            playerDieEvent.Register(player.NetworkObject);
        }

        public override void EnterState()
        {
            base.EnterState();

            playerDieEvent?.Broadcast();
        }

        private void Die(NoneParams param)
        {
            ragdoll.SetActive(true);
            ragdoll.AddForce(0f * health.HitDir, ForceMode.Impulse);
        }
    }
}
