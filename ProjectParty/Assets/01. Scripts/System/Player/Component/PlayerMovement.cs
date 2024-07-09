using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerMovement : CharacterMovement
    {
        private ExtendedAnimator anim;

        [Space]
        [SerializeField] private float animParamLerpTime;

        private int isGroundHash = Animator.StringToHash("isGround");
        private int isMoveHash = Animator.StringToHash("is_move");

        [Space]
        public PlayerMoveType MoveType;

        private bool isInit;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            anim = controller.GetCharacterComponent<PlayerVisual>().Anim;

            Movement.OnIsGroundChagend.AddListener(PlayerMovement_OnIsGroundChagend);
            Movement.OnMoveDirectionChanged.AddListener(PlayerMovement_OnMoveDirectionChanged);

            isInit = true;
        }

        public override void UpdateCompo()
        {
            base.UpdateCompo();
            
            Gravity();
            Movement.CheckGround();
        }

        private void OnEnable()
        {
            if(isInit)
                ChangeIsGroundParam(Movement.IsGround);
        }

        private void OnDestroy()
        {
            Movement.OnIsGroundChagend.RemoveListener(PlayerMovement_OnIsGroundChagend);
            Movement.OnMoveDirectionChanged.RemoveListener(PlayerMovement_OnMoveDirectionChanged);
        }

        public override void SetMoveDirection(Vector3 value, bool lookMoveDir = true, bool forceSet = false)
        {
            if (!isInit)
                return;

            Vector3 moveDir = value;

            switch(MoveType)
            {
                case PlayerMoveType.TopDown:
                    break;
                case PlayerMoveType.TPS:
                    {
                        TPSPlayerCamera tpsCamera = Controller.GetCharacterComponent<TPSPlayerCamera>();
                        if (tpsCamera == null)
                            break;

                        moveDir = tpsCamera.Forward * moveDir;
                    }
                break;
            }
            
            base.SetMoveDirection(moveDir, lookMoveDir, forceSet);
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
            anim.SetBool(isMoveHash, moveDir.sqrMagnitude > 0);
        }
    }
}