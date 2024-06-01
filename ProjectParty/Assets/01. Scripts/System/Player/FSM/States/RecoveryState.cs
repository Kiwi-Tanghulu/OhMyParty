using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using OMG.Client.Component;
using OMG.FSM;
using OMG.Player;
using OMG.Ragdoll;

namespace OMG.Player.FSM
{
    public class RecoveryState : PlayerFSMState
    {
        [SerializeField] private LayerMask groundLayer;

        private CharacterMovement movement;
        private ExtendedAnimator anim;
        private RagdollController ragdoll;

        private readonly int recoveryDirHash = Animator.StringToHash("recoveryDir");
        private readonly string recoverySelectAnim = "RecoverySelect";

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();
            anim = player.Animator;
            ragdoll = player.Visual.Ragdoll;
        }

        protected override void OwnerEnterState()
        {
            player.Visual.Ragdoll.SetActive(false);

            RaycastHit[] hit = Physics.RaycastAll(ragdoll.HipRb.transform.position + Vector3.up * 1000f, Vector3.down, 10000f, groundLayer);
            if(hit.Length > 0)
            {
                movement.Teleport(hit[0].point, transform.rotation);
            }

            anim.PlayAnim(recoverySelectAnim, AnimatorLayerType.Default);

            int recoDir = ragdoll.HipRb.transform.forward.y > 0f ? 1 : -1;
            anim.SetInt(recoveryDirHash, recoDir);

            base.OwnerEnterState();
        }
    }
}