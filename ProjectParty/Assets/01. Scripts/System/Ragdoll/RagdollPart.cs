using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class RagdollPart : MonoBehaviour
    {
        private Transform copyTarget;
        private Rigidbody rb;
        private Collider col;

        public Rigidbody Rb => rb;
        public Collider Col => col;

        public void Init(Transform copyTarget)
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();

            this.copyTarget = copyTarget;
        }

        public void Copy()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            transform.position = copyTarget.position;
            transform.rotation = copyTarget.rotation;
        }
    }
}