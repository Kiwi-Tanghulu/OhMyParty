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
        private CharacterMovement movement;
        private ExtendedAnimator anim;

        [SerializeField] private LayerMask groundLayer;

        private readonly int fallenDirHash = Animator.StringToHash("fallen_dir");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();
            health = player.GetComponent<PlayerHealth>();
            anim = player.Animator;
            ragdoll = player.Visual.Ragdoll;
        }

        public override void EnterState()
        {
            base.EnterState();

            ragdoll.SetActive(true);
            ragdoll.AddForce(health.Damage * health.HitDir, ForceMode.Impulse);
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            player.Visual.Ragdoll.SetActive(false);

            RaycastHit[] hit = Physics.RaycastAll(ragdoll.HipRb.transform.position, Vector3.down, 10000f, groundLayer);
            if (hit.Length > 0)
            {
                movement.Teleport(hit[0].point, transform.rotation);
            }

            int recoDir = ragdoll.HipRb.transform.forward.y > 0f ? 1 : -1;
            anim.SetInt(fallenDirHash, recoDir);
        }

        public override void ExitState()
        {
            base.ExitState();

            ragdoll.SetActive(false);
        }
    }
}