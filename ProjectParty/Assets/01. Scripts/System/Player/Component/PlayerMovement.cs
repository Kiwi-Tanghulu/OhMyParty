using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerMovement : CharacterMovement
    {
        private ExtendedAnimator anim;

        [Space]
        [SerializeField] private float animParamLerpTime;

        private int isGroundHash = Animator.StringToHash("isGround");
        private int moveSpeedHash = Animator.StringToHash("moveSpeed");

        private void Start()
        {
            anim = GetComponent<PlayerController>().Animator;

            OnIsGroundChagend += PlayerMovement_OnIsGroundChagend;
            OnMoveDirectionChanged += PlayerMovement_OnMoveDirectionChanged;
        }

        private void OnDestroy()
        {
            OnIsGroundChagend -= PlayerMovement_OnIsGroundChagend;
            OnMoveDirectionChanged -= PlayerMovement_OnMoveDirectionChanged;
        }

        protected override void Update()
        {
            base.Update();

            anim.SetFloat(moveSpeedHash, MoveDir.sqrMagnitude, true, animParamLerpTime);
        }

        private void PlayerMovement_OnIsGroundChagend(bool isGround)
        {
            ChangeIsGroundParam(isGround);
        }

        private void PlayerMovement_OnMoveDirectionChanged(Vector3 moveDir)
        {
            ChangeMoveSpeedParam(moveDir);
        }

        private void ChangeIsGroundParam(bool value)
        {
            anim.SetBool(isGroundHash, value);
        }

        private void ChangeMoveSpeedParam(Vector3 moveDir)
        {
            anim.SetFloat(moveSpeedHash, moveDir.sqrMagnitude, true, animParamLerpTime);
        }
    }
}