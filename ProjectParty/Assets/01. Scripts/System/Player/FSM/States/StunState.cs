using UnityEngine;
using OMG.FSM;
using OMG.Ragdoll;
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

        public NetworkEvent OnStunNetworkEvent = new NetworkEvent("OnStunNetworkEvent");

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            movement = player.GetCharacterComponent<PlayerMovement>();
            health = player.GetCharacterComponent<PlayerHealth>();
            anim = player.GetCharacterComponent<PlayerVisual>().Anim;
            ragdoll = player.GetCharacterComponent<PlayerVisual>().Ragdoll;

            if (brain.Controller.IsSpawned)
            {
                OnStunNetworkEvent.Register(player.GetComponent<NetworkObject>());
            }
        }

        public override void EnterState()
        {
            base.EnterState();

            ragdoll.SetActive(true);
            ragdoll.AddForce(health.Damage, health.HitDir);
            brain.Controller.InvokeNetworkEvent(OnStunNetworkEvent);

            movement.SetMoveDirection(Vector3.zero, false);
        }

        public override void ExitState()
        {
            base.ExitState();

            ragdoll.SetActive(false);

            RaycastHit[] hit = Physics.RaycastAll(ragdoll.HipRb.transform.position, Vector3.down, 10000f, groundLayer);
            if (hit.Length > 0)
            {
                movement.Teleport(hit[0].point, transform.rotation);
            }

            int recoDir = ragdoll.HipRb.transform.forward.y > 0f ? 1 : -1;
            anim.SetInt(fallenDirHash, recoDir);
        }

        private void OnDestroy()
        {
            if (brain.Controller.IsSpawned)
            {
                OnStunNetworkEvent.Unregister();
            }
        }
    }
}