using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class LoopMovePlatformGimmick : Gimmick
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Vector3 moveDir;
        [SerializeField] private float maxMoveDistance;
        private float moveDistance;

        private void Start()
        {
            moveDir.Normalize();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if(moveDistance >= maxMoveDistance)
            {
                moveDir *= -1f;
                moveDistance = 0f;
            }

            transform.position += moveDir * moveSpeed * Time.deltaTime;
            moveDistance += moveSpeed * Time.deltaTime;
        }

        protected override bool IsExecutable()
        {
            return true;
        }
    }
}