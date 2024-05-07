using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class CopyMotion : MonoBehaviour
    {
        [SerializeField] private Transform copyTrm;
        private float weight;

        private ConfigurableJoint joint;

        private Vector3 MirrorAnchorPosition;
        private Quaternion MirrorAnchorRotation;

        private void Awake()
        {
            joint = GetComponent<ConfigurableJoint>();

            if (copyTrm.name != transform.name)
                Debug.Log($"wrong sync object : {transform.name}");

            MirrorAnchorPosition = copyTrm.position;
            MirrorAnchorRotation = copyTrm.rotation;
        }

        private void Update()
        {
            Vector3 MirrorTargetPosition = GetTargetPosition(copyTrm.transform.position, MirrorAnchorPosition);
            joint.targetPosition = MirrorTargetPosition * weight;

            Quaternion MirrorTargetRotation = GetTargetRotation(copyTrm.transform.rotation, MirrorAnchorRotation);
            joint.targetRotation = Quaternion.Lerp(Quaternion.identity, MirrorTargetRotation, weight);
        }

        public void SetCopyMotionWeight(float value)
        {
            value = Mathf.Clamp(value, 0f, 1f);

            weight = value;
        }

        Vector3 GetTargetPosition(Vector3 currentPosition, Vector3 anchorPosition)
        {
            return anchorPosition - currentPosition;
        }

        Quaternion GetTargetRotation(Quaternion currentRotation, Quaternion anchorRotation)
        {
            return Quaternion.Inverse(currentRotation) * anchorRotation;
        }
    }
}