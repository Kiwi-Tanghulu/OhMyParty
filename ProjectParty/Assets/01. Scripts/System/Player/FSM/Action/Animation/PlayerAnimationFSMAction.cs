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

        protected ExtendedAnimator anim;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            anim = player.Visual.GetComponent<ExtendedAnimator>();
            hash = Animator.StringToHash(proertyName);
        }
    }
}