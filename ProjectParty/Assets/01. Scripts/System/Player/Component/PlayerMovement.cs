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
        private Vector3 horiVelocity;

        [Header("Turn")]
        [Space]
        [SerializeField] private float turnTime;
        private Coroutine trunCo;

        [Header("Jump")]
        [SerializeField] private float jumpPower = 5f;

        [Header("Gravity")]
        [Space]
        [SerializeField] private float gravityScale;
        private const float GRAVITY = -9.81f;
        public float GravityScale => GRAVITY * gravityScale;
        

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
        }

        #region Move
        public void Move()
        {
            Vector3 velocity = new Vector3(horiVelocity.x, rb.velocity.y, horiVelocity.z);

            rb.velocity = velocity;
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
            Quaternion start = transform.localRotation;
            Quaternion end = Quaternion.AngleAxis(Mathf.Atan2(lookVector.x, lookVector.z) * Mathf.Rad2Deg, Vector3.up);

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / turnTime;
                transform.localRotation = Quaternion.Lerp(start, end, t);

                yield return null;
            }
            transform.localRotation = end;
        }
        #endregion

        #region Jump
        public void Jump(float jumpPower = -1)
        {
            if (!IsGround)
                return;

            if (jumpPower == -1)
                jumpPower = this.jumpPower; 

            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        #endregion

        #region Gravity
        public void Gravity()
        {
            //if (!IsOwner)
            //    return;

            //isGround = CheckGround();

            //if (isGround && verticalVelocity <= 0f)
            //{
            //    verticalVelocity = GravityScale * 0.3f * Time.deltaTime;
            //}
            //else
            //{
            //    verticalVelocity += GravityScale * Time.deltaTime;
            //}

            //Vector3 velocity = rb.velocity;
            //velocity.y = verticalVelocity;
            //rb.velocity = velocity;
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
