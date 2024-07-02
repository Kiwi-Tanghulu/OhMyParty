using UnityEngine;
using OMG.FSM;
using OMG.Ragdoll;
using OMG.NetworkEvents;
using Unity.Netcode;

using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Player.FSM
{
    public class StunState : PlayerFSMState
    {
        private RagdollController ragdoll;
        private PlayerHealth health;
        private CharacterMovement movement;
        private ExtendedAnimator anim;

        [SerializeField] private LayerMask groundLayer;

        private readonly int fallenDirHash = Animator.StringToHash("fallen_dir");

        private NetworkEvent onStartStunEvent = new NetworkEvent("StartStunEvent");
        private NetworkEvent onEndStunEvent = new NetworkEvent("EndStunEvent");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();
            health = player.GetComponent<PlayerHealth>();
            anim = player.Animator;
            ragdoll = player.Visual.Ragdoll;
        }

        public override void NetworkInit()
        {
            base.NetworkInit();

            onStartStunEvent.AddListener(StratStun);
            onEndStunEvent.AddListener(EndStun);

            onStartStunEvent.Register(player.GetComponent<NetworkObject>());
            onEndStunEvent.Register(player.GetComponent<NetworkObject>());
        }

        public override void EnterState()
        {
            base.EnterState();

            if (brain.IsNetworkInit)
            {
                onStartStunEvent.Broadcast();
            }
            else
            {
                StratStun(new NoneParams());
            }
            
            movement.SetMoveDirection(Vector3.zero, false);
        }

        public override void ExitState()
        {
            base.ExitState();

            if (brain.IsNetworkInit)
            {
                onEndStunEvent.Broadcast();
            }
            else
            {
                EndStun(new NoneParams());
            }

            RaycastHit[] hit = Physics.RaycastAll(ragdoll.HipRb.transform.position, Vector3.down, 10000f, groundLayer);
            if (hit.Length > 0)
            {
                movement.Teleport(hit[0].point, transform.rotation);
            }

            int recoDir = ragdoll.HipRb.transform.forward.y > 0f ? 1 : -1;
            anim.SetInt(fallenDirHash, recoDir);
        }

        private void StratStun(NoneParams param)
        {
            ragdoll.SetActive(true);
            ragdoll.AddForce(health.Damage * health.HitDir, ForceMode.Impulse);
        }
        
        private void EndStun(NoneParams param)
        {
            player.Visual.Ragdoll.SetActive(false);
            ragdoll.SetActive(false);
        }
    }
}