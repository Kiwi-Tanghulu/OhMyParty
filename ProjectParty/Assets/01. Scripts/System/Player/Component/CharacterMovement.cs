using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : NetworkBehaviour
    {
        [Header("Move")]
        [SerializeField] private float moveSpeed = 3f;
        public float MoveSpeed => moveSpeed;
        private Vector3 moveDir;
        public Vector3 MoveDir => moveDir;
        private Vector3 moveVector;

        [Header("Gravity")]
        [SerializeField] private float gravityScale;
        //private bool applyGravity;
        //public bool ApplyGravity
        //{
        //    get 
        //    { 
        //        return applyGravity;
        //    }
        //    set
        //    {
        //        applyGravity = value;

        //        if (!applyGravity)
        //            SetVerticalVelocity(0f);
        //    }
        //}

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
        public bool DrawGizmo;

        [Header("Component")]
        private NetworkTransform networkTrm;
        private CharacterController cc;

        private void Awake()
        {
            networkTrm = GetComponent<NetworkTransform>();
            cc = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //Gravity();

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

            moveVec = moveDir * moveSpeed;

            moveVector = new Vector3(moveVec.x, 0f, moveVec.z) * Time.deltaTime;
        }

        public void SetMoveSpeed(float value)
        {
            moveSpeed = value;
        }

        public void SetMoveDirection(Vector3 value, bool lookMoveDir = true)
        {
            moveDir = value;

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
            //if (!applyGravity) return;

            if(isGround)
            {
                if(verticalVelocity < 0f)
                {
                    verticalVelocity = gravityScale * Time.deltaTime * 0.5f;
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
            
            SetVerticalVelocity(jumpPower);
        }
        #endregion

        #region Check Ground
        public bool CheckGround()
        {
            bool result = Physics.CheckBox(transform.position + checkGroundOffset,
                checkGroundHalfSize, Quaternion.identity, checkGroundLayer);
            isGround = result && verticalVelocity <= 0f;
            
            return isGround;
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