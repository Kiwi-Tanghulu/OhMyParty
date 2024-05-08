using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("Move")]
        [SerializeField] private float moveSpeed = 3f;
        public float MoveSpeed => moveSpeed;
        private Vector3 moveDir;
        public Vector3 MoveDir => moveDir;
        public bool ApplyMove;
        private Vector3 horiVelocity;

        [Header("Turn")]
        [Space]
        [SerializeField] private float turnTime;
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
        private Collider col;
        public Collider Collider => col;
        private Rigidbody rb;
        public Rigidbody Rigidbody => rb;

        private void Awake()
        {
            networkTrm = GetComponent<NetworkTransform>();
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
        }

        private void Update()
        {
            CheckGround();
            Move();
        }

        #region Move
        private void Move()
        {
            if(!ApplyMove)
                SetHorizontalVelocity(Vector3.zero, moveSpeed, false);

            rb.velocity = horiVelocity + Vector3.up * rb.velocity.y;
        }

        public void SetMoveDir(Vector3 moveDir, bool lookMoveDir = true)
        {
            SetHorizontalVelocity(moveDir, moveSpeed, lookMoveDir);
        }

        public void SetMoveSpeed(float speed)
        {
            SetHorizontalVelocity(MoveDir, speed);
        }
        
        public void SetHorizontalVelocity(Vector3 moveDir, float speed, bool lookMoveDir = true)
        {
            moveDir.y = 0f;
            this.moveDir = moveDir.normalized;
            moveSpeed = speed;

            horiVelocity = moveDir * moveSpeed;

            if (lookMoveDir)
                Look(moveDir);
        }

        public void Teleport(Vector3 pos)
        {
            networkTrm.Teleport(pos, transform.rotation, transform.localScale);
        }
        #endregion

        #region Turn
        private void Look(Vector3 lookVector)
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
                t += Time.deltaTime / turnTime;
                transform.rotation = Quaternion.Lerp(start, end, t);

                yield return null;
            }
            transform.rotation = end;
        }
        #endregion

        #region Vertical Velocity
        public void SetVerticalVelocity(float value)
        {
            Vector3 velocity = rb.velocity;
            velocity.y = value;
            rb.velocity = velocity;
        }

        public void Jump(float jumpPower = -1)
        {
            if (!IsGround)
                return;

            if (jumpPower == -1)
                jumpPower = this.jumpPower;

            SetVerticalVelocity(jumpPower);
        }
        #endregion

        #region Check Ground
        public bool CheckGround()
        {
            bool result = Physics.CheckBox(transform.position + checkGroundOffset,
                checkGroundHalfSize, Quaternion.identity, checkGroundLayer);
            isGround = result && rb.velocity.y <= 0f;
            
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
    }
}