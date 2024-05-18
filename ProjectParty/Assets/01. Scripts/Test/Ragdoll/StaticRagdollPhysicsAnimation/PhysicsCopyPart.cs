using UnityEngine;

namespace OMG.Ragdoll
{
    public class PhysicsCopyPart : CopyPart
    {
        private Transform root;

        private ConfigurableJoint joint;
        private Vector3 originRootPos;

        public override void Init(Transform animRoot, Transform ragdollRoot)
        {
            base.Init(animRoot, ragdollRoot);
            
            joint = GetComponent<ConfigurableJoint>();

            root = ragdollRoot;
            originRootPos = root.position;
        }

        private void FixedUpdate()
        {
            if (root == null)
                return;
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