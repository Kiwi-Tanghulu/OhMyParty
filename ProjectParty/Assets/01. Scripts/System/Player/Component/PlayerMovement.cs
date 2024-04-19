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
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        private Vector3 moveDir;
        public Vector3 MoveDir => moveDir;
        private float verticalVelocity;

        [Header("Turn")]
        [Space]
        [SerializeField] private float turnTime;
        private Coroutine trunCo;

        [Header("Jump")]
        [SerializeField] private float jumpPower;

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
        private Rigidbody rb;
        private CapsuleCollider col;
        public Rigidbody Rigidbody => rb;
        public CapsuleCollider Collider => col;

        private void Awake()
        {
            networkTrm = GetComponent<NetworkTransform>();
            rb = GetComponent<Rigidbody>();
        }

        #region Move
        public void SetMoveDir(Vector3 moveDir, bool turn = true)
        {
            this.moveDir = moveDir.normalized;

            if (turn)
            {
                if (moveDir != Vector3.zero)
                {
                    if (trunCo != null)
                        StopCoroutine(trunCo);
                    trunCo = StartCoroutine(TurnCo());
                }
                else
                {
                    if (trunCo != null)
                        StopCoroutine(trunCo);
                }
            }
        }

        public void Move()
        {
            Vector3 horizontalVelocity = moveDir * moveSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }

        public void Teleport(Vector3 pos)
        {
            networkTrm.Teleport(pos, transform.rotation, transform.localScale);
        }
        #endregion

        #region Turn
        private IEnumerator TurnCo()
        {
            float t = 0f;
            Quaternion start = transform.localRotation;
            Quaternion end = Quaternion.AngleAxis(Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg, Vector3.up);

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / turnTime;
                transform.localRotation = Quaternion.Lerp(start, end, t);

                yield return null;
            }
            transform.localRotation = end;
        }
        #endregion

        #region
        public void Jump()
        {
            verticalVelocity += jumpPower;
        }
        #endregion

        #region Gravity
        public void Gravity()
        {
            if (!IsOwner)
                return;

            isGround = CheckGround();

            if (isGround && verticalVelocity < 0f)
            {
                verticalVelocity = GravityScale * 0.3f;
            }
            else
            {
                verticalVelocity += GravityScale;
            }

            Vector3 velocity = rb.velocity;
            velocity.y = verticalVelocity * Time.deltaTime;
            rb.velocity = velocity;
        }
        #endregion

        #region Check Ground
        public bool CheckGround()
        {
            bool result = Physics.CheckBox(transform.position + checkGroundOffset,
                checkGroundHalfSize, Quaternion.identity, checkGroundLayer);

            return result;
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
