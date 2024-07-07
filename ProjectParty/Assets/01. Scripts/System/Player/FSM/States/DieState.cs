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

        public NetworkEvent playerDieEvent = new NetworkEvent("PlayerDieEvent");

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            health = player.GetComponent<PlayerHealth>();
            ragdoll = player.GetCharacterComponent<PlayerVisual>().Ragdoll;

            if(brain.Controller.IsSpawned)
            {
                playerDieEvent.AddListener(Die);
                playerDieEvent.Register(player.NetworkObject);
            }
        }

        public override void EnterState()
        {
            base.EnterState();

            playerDieEvent?.Broadcast();
        }

        private void Die(NoneParams param)
        {
            ragdoll.SetActive(true);
            ragdoll.AddForce(health.Damage, health.HitDir, ForceMode.Impulse);
        }
    }
}
