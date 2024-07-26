using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace OMG
{
    public class MovementComponent : MonoBehaviour
    {
        private CharacterStatSO statSO;
        public CharacterStatSO StatSO => statSO;

        //move
        [Header("Move")]
        protected float moveSpeed;
        protected Vector3 moveDir;
        protected Vector3 prevMoveDir;
        protected Vector3 moveVector;
        public bool EnableMove;
        public Vector3 MoveDir => moveDir;
        public UnityEvent<Vector3> OnMoveDirectionChanged;

        //vertical
        private float verticalVelocity;

        //turn
        private Coroutine trunCo;

        //ground
        [Space]
        [Header("Ground Check")]
        [SerializeField] private Vector3 checkGroundOffset;
        [SerializeField] private float checkGroundRadius;
        [SerializeField] private LayerMask checkGroundLayer;
        private bool isGround;
        public bool IsGround => isGround;
        public UnityEvent<bool> OnIsGroundChagend;
        public bool DrawGizmo;

        //gravity
        public bool EnableGravity;

        //compo
        private NetworkTransform networkTrm;

        public virtual void Init(CharacterStatSO statSO)
        {
            networkTrm = GetComponent<NetworkTransform>();
            EnableMove = true;
            EnableGravity = true;
            this.statSO = statSO;
        }

        public virtual void Move()
        {
            if (!EnableMove)
                return;
            
            CalcMoveVector();
        }

        protected virtual void CalcMoveVector()
        {
            Vector3 moveVec = Vector3.zero;
            
            if (moveDir != Vector3.zero)
            {
                if (prevMoveDir != Vector3.zero)
                {
                    if (Mathf.Acos(Vector3.Dot(prevMoveDir, MoveDir)) * Mathf.Rad2Deg > 90f)
                    {
                        moveSpeed = 0f;
                    }
                }

                moveSpeed += statSO[CharacterStatType.Accelration].Value * Time.deltaTime;

                moveVec = moveDir;
            }
            else
            {
                moveSpeed -= statSO[CharacterStatType.Accelration].Value * Time.deltaTime;

                moveVec = prevMoveDir;
            }

            moveSpeed = Mathf.Clamp(moveSpeed, 0f, statSO[CharacterStatType.MaxMoveSpeed].Value);

            moveVec *= moveSpeed;
            
            moveVector = new Vector3(moveVec.x, verticalVelocity, moveVec.z);
        }

        public void SetMoveSpeed(float value)
        {
            moveSpeed = value;
        }

        public virtual void SetMoveDirection(Vector3 value, bool lookMoveDir = true, bool forceSet = false)
        {
            prevMoveDir = forceSet ? value : moveDir;
            moveDir = value;

            OnMoveDirectionChanged?.Invoke(moveDir);

            if (lookMoveDir)
                Turn(moveDir);
        }

        public void Teleport(Vector3 pos, Quaternion rot)
        {
            networkTrm.Teleport(pos, rot, transform.localScale);
        }


        public virtual void Turn(Vector3 lookVector)
        {
            if (!gameObject.activeSelf)
                return;

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

        protected IEnumerator TurnCo(Vector3 lookVector)
        {
            float t = 0f;
            Quaternion start = transform.rotation;
            Quaternion end = Quaternion.AngleAxis(Mathf.Atan2(lookVector.x, lookVector.z) * Mathf.Rad2Deg, Vector3.up);

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime * statSO[CharacterStatType.TurnSpeed].Value;
                transform.rotation = Quaternion.Lerp(start, end, t);

                yield return null;
            }
            transform.rotation = end;
        }


        public virtual void Jump()
        {
            if(!EnableGravity)
            {
                Debug.LogError("should enable gravity for jump");
                return;
            }

            verticalVelocity = statSO[CharacterStatType.JumpPower].Value;
        }

        public virtual void Jump(float power)
        {
            if (!EnableGravity)
            {
                Debug.LogError("should enable gravity for jump");
                return;
            }

            verticalVelocity = power;
        }

        public virtual void Gravity()
        {
            if (!EnableGravity)
                return;

            if (isGround)
            {
                if (verticalVelocity < 0f)
                {
                    verticalVelocity = statSO[CharacterStatType.GravityScale].Value * Time.deltaTime * 20f;
                }
            }
            else
            {
                verticalVelocity += statSO[CharacterStatType.GravityScale].Value * Time.deltaTime;
            }
        }


        public virtual bool CheckGround()
        {
            bool result = Physics.CheckSphere(transform.position + checkGroundOffset,
                checkGroundRadius, checkGroundLayer) && verticalVelocity <= 0f;

            if (isGround != result)
            {
                isGround = result;

                OnIsGroundChagend?.Invoke(isGround);
            }

            return isGround;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (DrawGizmo)
            {
                Gizmos.color = isGround ? Color.green : Color.red;
                Gizmos.DrawWireSphere(transform.position + checkGroundOffset, checkGroundRadius);
            }
        }
#endif
    }
}