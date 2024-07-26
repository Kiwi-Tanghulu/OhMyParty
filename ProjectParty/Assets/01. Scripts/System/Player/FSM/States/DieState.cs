using OMG.FSM;
using OMG.NetworkEvents;
using OMG.Ragdoll;

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
               playerDieEvent.Register(player.NetworkObject);
        }

        public override void EnterState()
        {
            base.EnterState();

            ragdoll.SetActive(true);
            ragdoll.AddForce(health.Damage, health.HitDir);

#if UNITY_EDOTR
            if(brain.Controller.UseInNetwork)
                playerDieEvent?.Broadcast();
            else
                playerDieEvent?.Invoke();

            return;
#else
            playerDieEvent?.Broadcast();
#endif
        }
    }
}
