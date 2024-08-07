using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovementComponent : MovementComponent
    {
        protected Rigidbody rb;
        protected Collider col;

        public override void Init(CharacterStatSO statSO)
        {
            base.Init(statSO);

            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
        }

        public override void Move()
        {
            base.Move();

            Vector3 velocity = rb.velocity;
            velocity.x = moveVector.x;
            velocity.z = moveVector.z;

            rb.velocity = velocity;
        }

        public override void VerticalMove()
        {
            base.VerticalMove();

            Vector3 velocity = rb.velocity;
            velocity.y = VerticalVelocity;

            rb.velocity = velocity;
        }

        public override void SetCollisionActive(bool active)
        {
            rb.isKinematic = !active;
            col.enabled = active;
        }
    }
}
