using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        private Vector3 moveDir;

        public Vector3 MoveDir => moveDir;

        public void SetMoveDir(Vector3 moveDir)
        {
            this.moveDir = moveDir.normalized;
        }

        public void Move()
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            Debug.Log("move player");
        }
    }
}
