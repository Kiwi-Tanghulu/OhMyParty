using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class RagdollPart : MonoBehaviour
    {
        private Transform root;

        private ConfigurableJoint joint;

        private Vector3 originPos;
        private Quaternion originRot;

        public void Init(Transform root)
        {
            this.root = root;

            joint = GetComponent<ConfigurableJoint>();

            originPos = transform.localPosition;
            originRot = transform.localRotation;
        }

        private void Update()
        {
            if (root == null)
                return;

            joint.targetPosition = -root.position;
        }

        public Vector3 GetCopyPos(float weight)
        {
            weight = Mathf.Clamp(weight, 0f, 1f);

            return (originPos - transform.localPosition) * weight;
        }

        public Quaternion GetCopyRot(float weight)
        {
            weight = Mathf.Clamp(weight, 0f, 1f);

            return Quaternion.Lerp(Quaternion.Euler(Vector3.zero),
                Quaternion.Inverse(Quaternion.Inverse(transform.localRotation) * originRot), weight);
        }
    }
}