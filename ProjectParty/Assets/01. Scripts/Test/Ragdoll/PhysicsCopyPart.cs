using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class PhysicsCopyPart : CopyPart
    {
        private ConfigurableJoint joint;

        public override void Init(Transform root)
        {
            base.Init(root);

            joint = GetComponent<ConfigurableJoint>();
        }

        private void FixedUpdate()
        {
            if (root == null)
                return;

            Vector3 targetPosition = -root.position;

            joint.targetPosition = targetPosition;
        }

        public override Vector3 GetCopyPosition(float weight)
        {
            return (originPos - transform.localPosition) * weight;
        }

        public override Quaternion GetCopyRotation(float weight)
        {
            return Quaternion.Lerp(Quaternion.Euler(Vector3.zero),
                Quaternion.Inverse(Quaternion.Inverse(transform.localRotation) * originRot), weight);
        }
    }
}