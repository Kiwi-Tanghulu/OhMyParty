using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class RecoveryState : FSMState
    {
        private PlayerRagdoll ragdoll;

        int recoveryAnimHash = Animator.StringToHash("recovery");
        int recoveryDirAnimHash = Animator.StringToHash("recoveryDir");

        public override void InitState(ActioningPlayer actioningPlayer)
        {
            base.InitState(actioningPlayer);

            ragdoll = anim.GetComponent<PlayerRagdoll>();
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.SetTrigger(recoveryAnimHash);
            anim.SetInt(recoveryDirAnimHash, (int)Mathf.Sign(ragdoll.HipTrm.forward.y));
        }
    }
}