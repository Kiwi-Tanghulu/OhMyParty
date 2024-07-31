using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovementComponent : MovementComponent
    {
        protected Rigidbody rb;

        public override void Init(CharacterStatSO statSO)
        {
            base.Init(statSO);

            rb = GetComponent<Rigidbody>();
        }

        public override void Move()
        {
            base.Move();

            rb.velocity = moveVector;
        }
    }
}
