using OMG.Minigames.MazeAdventure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float maxMoveSpeed = 3f;
        public float MaxMoveSpeed => maxMoveSpeed;
        private float currentMoveSpeed;
        [SerializeField]
        private float accelration;

        private Vector3 moveDir;
        public Vector3 MoveDir => moveDir;
        private Vector3 prevMoveDir;

        private Vector3 moveVector;

        public UnityEvent<Vector3> OnMoveDirectionChanged;

        [Header("Gravity")]
        [SerializeField] private float gravityScale;

        private float verticalVelocity;

        [Header("Turn")]
        [Space]
        [SerializeField] private float turnSpped;
        private Coroutine trunCo;

        [Header("Jump")]
        [SerializeField] private float jumpPower = 5f;

        [Header("Ground Check")]
        [Space]
        [SerializeField] private Vector3 checkGroundOffset;
        [SerializeField] private Vector3 checkGroundHalfSize;
        [SerializeField] private LayerMask checkGroundLayer;

        private bool isGround;
        public bool IsGround => isGround;

        public UnityEvent<bool> OnIsGroundChagend;
        public bool DrawGizmo;

        [Header("Component")]
        private NetworkTransform networkTrm;
        private CharacterController cc;

        public UnityEvent<Collider> OnColliderHit;
        protected virtual void Awake()
        {
            networkTrm = GetComponent<NetworkTransform>();
            cc = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            CheckGround();
        }

        #region Move
        public void Move()
        {
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

                currentMoveSpeed += accelration * Time.deltaTime;

                moveVec = moveDir;
            }
            else
            {
                currentMoveSpeed -= accelration * Time.deltaTime;

                moveVec = prevMoveDir;
            }

            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0f, MaxMoveSpeed);

            moveVec *= currentMoveSpeed;

            moveVector = new Vector3(moveVec.x, 0f, moveVec.z) * Time.deltaTime;
        }

        public void SetMoveSpeed(float value)
        {
            maxMoveSpeed = value;
        }

        public void SetMoveDirection(Vector3 value, bool lookMoveDir = true)
        {
            prevMoveDir = moveDir;
            moveDir = value;

            OnMoveDirectionChanged?.Invoke(moveDir);

            if (value != Vector3.zero && lookMoveDir)
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
                t += Time.deltaTime * turnSpped;
                transform.rotation = Quaternion.Lerp(start, end, t);

                yield return null;
            }
            transform.rotation = end;
        }
        #endregion

        #region Vertical Velocity
        public void Gravity()
        {
            if(isGround)
            {
                if(verticalVelocity < 0f)
                {
                    verticalVelocity = gravityScale * Time.deltaTime * 20f;
                }
            }
            else
            {
                verticalVelocity += gravityScale * Time.deltaTime;
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

            SetVerticalVelocity(jumpPower);
        }

        public void Jump(float jumpPower)
        {
            if (!IsGround) return;
            if (jumpPower < 0f) return;

            Debug.Log("jump");
            SetVerticalVelocity(jumpPower);
        }
        #endregion

        #region Check Ground
        public bool CheckGround()
        {
            bool result = Physics.CheckBox(transform.position + checkGroundOffset,
                checkGroundHalfSize, Quaternion.identity) && verticalVelocity <= 0f;

            if(isGround != result)
            {
                isGround = result;

                OnIsGroundChagend?.Invoke(isGround);
            }
            
            return isGround;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            OnColliderHit?.Invoke(hit.collider);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmo)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + checkGroundOffset, checkGroundHalfSize * 2);
        }
#endif
        #endregion

        #region ETC
        public void SetCollision(bool value)
        {
            cc.detectCollisions = value;
        }
        #endregion
    }
}