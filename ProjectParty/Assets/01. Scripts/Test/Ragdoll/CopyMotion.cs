using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class CopyMotion : MonoBehaviour
    {
        #region
        //[SerializeField] private Transform copyTrm;


        //private Vector3 MirrorAnchorPosition;
        //private Quaternion MirrorAnchorRotation;

        //private void Awake()
        //{
        //    MirrorAnchorPosition = copyTrm.position;
        //    MirrorAnchorRotation = copyTrm.rotation;

        //    SetCopyMotionWeight(1f);

        //    enabled = false;
        //}

        //private void Update()
        //{
        //    if (hardCopy)
        //    {
        //        //transform.localRotation = Quaternion.Lerp(transform.localRotation, copyTrm.localRotation,
        //        //    Time.deltaTime * hardCopyWeight);
        //        //transform.localPosition = Vector3.Lerp(transform.localPosition, copyTrm.localPosition,
        //        //    Time.deltaTime * hardCopyWeight);

        //        transform.localRotation = copyTrm.localRotation;
        //        transform.localPosition = copyTrm.localPosition;
        //    }
        //    else
        //    {
        //        Vector3 MirrorTargetPosition = GetTargetPosition(copyTrm.transform.position, MirrorAnchorPosition);
        //        joint.targetPosition = MirrorTargetPosition * weight;

        //        Quaternion MirrorTargetRotation = GetTargetRotation(copyTrm.transform.rotation, MirrorAnchorRotation);
        //        joint.targetRotation = Quaternion.Lerp(Quaternion.identity, MirrorTargetRotation, weight);
        //    }
        //}

        //public void SetCopyMotionWeight(float value)
        //{
        //    value = Mathf.Clamp(value, 0f, 1f);

        //    weight = value;
        //}
        #endregion

        [SerializeField] private Transform animCopyTrm;
        [SerializeField] private Transform ragdollCopyTrm;

        [SerializeField] private float ragdollCopyWeight;
        private Vector3 ragdollOriginPos;
        private Quaternion ragdollOriginRot;

        private void Awake()
        {
            ragdollOriginPos = ragdollCopyTrm.localPosition;
            ragdollOriginRot = ragdollCopyTrm.localRotation;

            ragdollCopyWeight = 1f;
        }

        private void Update()
        {
            Vector3 copyPos = animCopyTrm.localPosition + 
                GetRagdollCopyPos(ragdollCopyTrm.localPosition, ragdollOriginPos, ragdollCopyWeight);
            Quaternion copyRot = GetRagdollCopyRot(ragdollCopyTrm.localRotation, ragdollOriginRot, ragdollCopyWeight) 
                * animCopyTrm.localRotation;

            transform.localPosition = copyPos;
            transform.localRotation = copyRot;
        }
        
        private Vector3 GetRagdollCopyPos(Vector3 currentPosition, Vector3 anchorPosition, float weight)
        {
            weight = Mathf.Clamp(weight, 0f, 1f);

            return (anchorPosition - currentPosition) * weight;
        }

        private Quaternion GetRagdollCopyRot(Quaternion currentRotation, Quaternion anchorRotation, float weight)
        {
            weight = Mathf.Clamp(weight, 0f, 1f);

            return Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Inverse(currentRotation) * anchorRotation,
                weight);
        }
    }
}