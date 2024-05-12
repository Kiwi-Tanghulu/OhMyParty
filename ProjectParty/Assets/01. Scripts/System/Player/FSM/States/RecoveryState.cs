using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using OMG.Client.Component;
using OMG.FSM;
using OMG.Player;

namespace OMG.Player.FSM
{
    public class RecoveryState : PlayerFSMState
    {
        private PlayerMovement movement;
        private ExtendedAnimator anim;
        private PlayerRagdoll ragdoll;

        private readonly string frontRecoveryAnim = "Recovery(Front)";
        private readonly string backRecoveryAnim = "Recovery(Back)";

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<PlayerMovement>();
            anim = player.transform.Find("Visual").GetComponent<ExtendedAnimator>();
            ragdoll = player.transform.Find("Visual").GetComponent<PlayerRagdoll>();
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
        }
    }
}