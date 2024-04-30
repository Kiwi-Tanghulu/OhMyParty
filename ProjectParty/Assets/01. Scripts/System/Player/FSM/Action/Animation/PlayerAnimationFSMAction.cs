using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class PlayerAnimationFSMAction : PlayerFSMAction
    {
        [SerializeField] private string proertyName;

        protected int hash;

        protected PlayerAnimation anim;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            anim = player.Visual.GetComponent<PlayerAnimation>();
            hash = Animator.StringToHash(proertyName);
        }
    }
}