using System.Collections;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class CharacterMovement : CharacterComponent
    {
        private CharacterStatSO characterStatSO;

        //move
        private float currentMoveSpeed;

        private Vector3 moveDir;
        public Vector3 MoveDir => moveDir;
        private Vector3 prevMoveDir;

        private Vector3 moveVector;

        private bool enableMove;

        [Space]
        public UnityEvent<Vector3> OnMoveDirectionChanged;

        //vertical
        private float verticalVelocity;

        //turn
        private Coroutine trunCo;

        //ground
        [Header("Ground Check")]
        [Space]
        [SerializeField] private Vector3 checkGroundOffset;
        [SerializeField] private float checkGroundRadius;
        [SerializeField] private LayerMask checkGroundLayer;

        private bool isGround;
        public bool IsGround => isGround;

        private bool enableGravity;

        public UnityEvent<bool> OnIsGroundChagend;
        public bool DrawGizmo;

        //compo
        [Header("Component")]
        private NetworkTransform networkTrm;
        private UnityEngine.CharacterController cc;

        public UnityEvent<ControllerColliderHit> OnColliderHit;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            networkTrm = GetComponent<NetworkTransform>();
            cc = GetComponent<UnityEngine.CharacterController>();

            characterStatSO = GetComponent<CharacterStat>().StatSO;

            enableMove = true;
            enableGravity = true;
        }

        public override void UpdateCompo()
        {
            base.UpdateCompo();

            CheckGround();
            Move();
        }

        #region Move
        public void Move()
        {
            if (!enableMove)
                return;

            CalcMoveVector();

            cc.Move(moveVector);
        }

        private void CalcMoveVector()
        {
            Vector3 moveVec = Vector3.zero;

            if(moveDir != Vector3.zero)
            {
                if (prevMoveDir != Vector3.zero)
                {
                    if (Mathf.Acos(Vector3.Dot(prevMoveDir, MoveDir)) * Mathf.Rad2Deg > 90f)
                    {
                        currentMoveSpeed = 0f;
                    }
                }

                currentMoveSpeed += characterStatSO[CharacterStatType.Accelration].Value * Time.deltaTime;

                moveVec = moveDir;
            }
            else
            {
                currentMoveSpeed -= characterStatSO[CharacterStatType.Accelration].Value * Time.deltaTime;

                moveVec = prevMoveDir;
            }

            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0f, characterStatSO[CharacterStatType.MaxMoveSpeed].Value);

            moveVec *= currentMoveSpeed;

            moveVector = new Vector3(moveVec.x, 0f, moveVec.z) * Time.deltaTime;
        }

        public void SetMoveSpeed(float value)
        {
            currentMoveSpeed = value; //jiseong
            //characterStatSO.MaxMoveSpeed = value;
        }

        public void SetMoveDirection(Vector3 value, bool lookMoveDir = true)
        {
            prevMoveDir = moveDir;
            moveDir = value;

            OnMoveDirectionChanged?.Invoke(moveDir);
            
            if (/*value != Vector3.zero && */lookMoveDir)
                Turn(moveDir);
        }

        public void Teleport(Vector3 pos, Quaternion rot)
        {
            networkTrm.Teleport(pos, rot, transform.localScale);
        }
        #endregion

        #region Turn
        private void Turn(Vector3 lookVector)
        {
            lookVector.Normalize();

            if (lookVector != Vector3.zero)
            {
                if (trunCo != null)
                    StopCoroutine(trunCo);
                trunCo = StartCoroutine(TurnCo(lookVector));
            }
            else
            {
                if (trunCo != null)
                    StopCoroutine(trunCo);
            }
        }

        private IEnumerator TurnCo(Vector3 lookVector)
        {
            float t = 0f;
            Quaternion start = transform.rotation;
            Quaternion end = Quaternion.AngleAxis(Mathf.Atan2(lookVector.x, lookVector.z) * Mathf.Rad2Deg, Vector3.up);

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime * characterStatSO[CharacterStatType.TurnSpeed].Value;
                transform.rotation = Quaternion.Lerp(start, end, t);

                yield return null;
            }
            transform.rotation = end;
        }
        #endregion

        #region Vertical Velocity
        public void Gravity()
        {
            if (!enableGravity)
                return;

            if(isGround)
            {
                if(verticalVelocity < 0f)
                {
                    verticalVelocity = characterStatSO[CharacterStatType.GravityScale].Value * Time.deltaTime * 20f;
                }
            }
            else
            {
                verticalVelocity += characterStatSO[CharacterStatType.GravityScale].Value * Time.deltaTime;
            }

            cc.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        }

        public void SetVerticalVelocity(float value)
        {
            verticalVelocity = value;
        }

        public void Jump()
        {
            if (!IsGround) return;

            SetVerticalVelocity(characterStatSO[CharacterStatType.JumpPower].Value);
        }

        public void Jump(float jumpPower)
        {
            if (!IsGround) return;
            if (jumpPower < 0f) return;

            SetVerticalVelocity(jumpPower);
        }
        #endregion

        #region Check Ground
        public bool CheckGround()
        {
            bool result = Physics.CheckSphere(transform.position + checkGroundOffset,
                checkGroundRadius, checkGroundLayer) && verticalVelocity <= 0f;

            if(isGround != result)
            {
                isGround = result;

                OnIsGroundChagend?.Invoke(isGround);
            }
            
            return isGround;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            OnColliderHit?.Invoke(hit);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmo)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + checkGroundOffset, checkGroundRadius);
        }
#endif
        #endregion
    }
}