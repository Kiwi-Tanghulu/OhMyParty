using OMG.Client.Component;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Players
{
    public class RecoveryState : FSMState
    {
        private PlayerRagdoll ragdoll;

        private readonly string frontRecoveryAnim = "Recovery(Front)";
        private readonly string backRecoveryAnim = "Recovery(Back)";

        public override void InitState(PlayerController actioningPlayer)
        {
            base.InitState(actioningPlayer);

            ragdoll = anim.GetComponent<PlayerRagdoll>();
        }

        public override void EnterState()
        {
            base.EnterState();

            movement.ApplyGravity = true;
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            RaycastHit[] hit = Physics.RaycastAll(ragdoll.HipTrm.position + Vector3.up * 1000f, Vector3.down, 10000f);
            if(hit.Length > 0)
            {
                movement.Teleport(hit[0].point);
            }

            string animName = ragdoll.HipTrm.forward.y > 0f ? frontRecoveryAnim : backRecoveryAnim;
            anim.PlayAnim(animName, AnimatorLayerType.Default);
            //actioningPlayer.transform.position = ragdoll.HipTrm.position;
        }
    }
}