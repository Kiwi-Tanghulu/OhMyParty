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
        [SerializeField] private float moveSpeed = 3f;
        private Vector3 moveDir;
        private float verticalVelocity;

        [Space]
        [SerializeField] private float turnTime;
        private Coroutine trunCo;

        [Space]
        [SerializeField] private float gravityScale;
        public bool ApplyGravity;

        [Space]
        [SerializeField] private Vector3 checkGroundOffset;
        [SerializeField] private Vector3 checkGroundHalfSize;
        [SerializeField] private LayerMask checkGroundLayer;

        public bool DrawGizmo;

        public Vector3 MoveDir => moveDir;

        private CharacterController controller;
        public CharacterController Controller => controller;

        private NetworkTransform networkTrm;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            networkTrm = GetComponent<NetworkTransform>();
        }

        private void Update()
        {
            Gravity();
        }

        public void SetMoveDir(Vector3 moveDir)
        {
            this.moveDir = moveDir.normalized;

            if(moveDir != Vector3.zero)
            {
                if (trunCo != null)
                    StopCoroutine(trunCo);
                trunCo = StartCoroutine(TurnCo());
            }
            else
            {
                if(trunCo != null)
                    StopCoroutine(trunCo);
            }
        }

        public void Move()
        {
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }

        public void Teleport(Vector3 pos)
        {
            networkTrm.Teleport(pos, transform.rotation, transform.localScale);
        }

        public void Gravity()
        {
            if (!IsOwner)
                return;
            if (!ApplyGravity)
                return;

            bool isGround = CheckGround();

            if(isGround)
            {
                verticalVelocity = gravityScale * 0.3f;
            }
            else
            {
                verticalVelocity += gravityScale;
            }

            controller.Move(Vector3.up * gravityScale * Time.deltaTime);
        }

        public bool CheckGround()
        {
            bool result = Physics.CheckBox(transform.position + checkGroundOffset,
                checkGroundHalfSize, Quaternion.identity, checkGroundLayer);
            
            return result;
        }

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


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmo)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + checkGroundOffset, checkGroundHalfSize * 2);
        }
#endif
    }
}
