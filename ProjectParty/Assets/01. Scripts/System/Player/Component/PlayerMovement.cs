using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerMovement : CharacterMovement
    {
        private ExtendedAnimator anim;

        private int isGroundHash = Animator.StringToHash("isGround");

        private void Start()
        {
            anim = GetComponent<PlayerController>().Animator;

            OnIsGroundChagend += PlayerMovement_OnIsGroundChagend;
        }

        private void PlayerMovement_OnIsGroundChagend(bool value)
        {
            ChangeIsGroundParam(value);
        }

        private void ChangeIsGroundParam(bool value)
        {
            anim.SetBool(isGroundHash, value);
        }
    }
}