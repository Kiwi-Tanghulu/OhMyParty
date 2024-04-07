using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        private Vector3 moveDir;

        [Space]
        [SerializeField] private float turnTime;
        private Coroutine trunCo;

        public Vector3 MoveDir => moveDir;

        private CharacterController controller;
        public CharacterController Controller => controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
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
        }

        public void Move()
        {
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
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
    }
}
