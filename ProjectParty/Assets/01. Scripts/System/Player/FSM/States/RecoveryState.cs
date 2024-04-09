using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class RecoveryState : FSMState
    {
        private PlayerRagdoll ragdoll;

        private readonly string frontRecoveryAnim = "Recovery(Front)";
        private readonly string backRecoveryAnim = "Recovery(Back)";

        public override void InitState(ActioningPlayer actioningPlayer)
        {
            base.InitState(actioningPlayer);

            ragdoll = anim.GetComponent<PlayerRagdoll>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            actioningPlayer.transform.position = ragdoll.HipTrm.position;
            actioningPlayer.transform.position -= actioningPlayer.transform.localPosition.y * Vector3.up;

            string animName = ragdoll.HipTrm.forward.y > 0f ? frontRecoveryAnim : backRecoveryAnim;
            anim.PlayAnim(animName, AnimatorLayerType.Default);
        }
    }
}