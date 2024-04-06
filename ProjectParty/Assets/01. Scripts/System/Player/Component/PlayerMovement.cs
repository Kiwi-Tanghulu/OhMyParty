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
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        private IEnumerator TurnCo()
        {
            float t = 0f;
            float start = transform.localEulerAngles.y;
            float end = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

            if (start > 180f)
                start -= 360f;
            if (end > 180f)
                end -= 360f;
            Debug.Log(end);

            while (1f - t > 0.1f)
            {
                t += Time.deltaTime / turnTime;
                transform.localEulerAngles = Vector3.up * Mathf.Lerp(start, end, t);
                Debug.Log(transform.eulerAngles.y);

                yield return null;
            }
            transform.localRotation = Quaternion.Euler(0f, end, 0f);
            Debug.Log(transform.eulerAngles.y);
        }
    }
}
