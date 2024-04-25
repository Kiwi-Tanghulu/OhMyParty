using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.FSM;
using OMG.Input;
using OMG.Player;

namespace OMG.Player.FSM
{
    public class IdleState : PlayerFSMState
    {
        [SerializeField] private PlayInputSO input;

        private PlayerMovement movement;
        private PlayerAnimation anim;

        private readonly int moveSpeedAnimHash = Animator.StringToHash("moveSpeed");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<PlayerMovement>();
            //anim = player.transform.Find("Visual").GetComponent<PlayerAnimation>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            //anim.SetFloat(moveSpeedAnimHash, 0f, true, 0.1f);
        }
    }
}