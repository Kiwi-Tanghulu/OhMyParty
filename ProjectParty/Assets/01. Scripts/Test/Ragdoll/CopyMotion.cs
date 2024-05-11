using OMG.Extensions;
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

        private CopyPart ragdollCopyPart;
        private CopyPart animCopyPart;

        private float ragdollCopyWeight;
        private float animCopyWeight;

        public void Init(Transform ragdollRoot, Transform animRoot, float ragdollCopyWeight, float animCopyWeight)
        {
            ragdollCopyPart = ragdollRoot.FindFromAll(transform.name).GetComponent<CopyPart>();
            animCopyPart = animRoot.FindFromAll(transform.name).GetComponent<CopyPart>();

            ragdollCopyPart.Init(ragdollRoot);
            animCopyPart.Init(animRoot);

            SetRagdollCopyWeight(ragdollCopyWeight);
            SetAnimationCopyWeight(animCopyWeight);
        }

        public void Update()
        {
            if(animCopyPart == null || ragdollCopyPart == null)
            {
                Debug.Log($"not setting copy motion component : {transform.name}");
                return;
            }

            //copy form : anim + ragdoll
            Vector3 copyPos = animCopyPart.GetCopyPosition(animCopyWeight) + ragdollCopyPart.GetCopyPosition(ragdollCopyWeight);
            Quaternion copyRot = ragdollCopyPart.GetCopyRotation(ragdollCopyWeight) * animCopyPart.GetCopyRotation(animCopyWeight);

            transform.localPosition = copyPos;
            transform.localRotation = copyRot;
        }

        public void SetRagdollCopyWeight(float wieght)
        {
            ragdollCopyWeight = wieght;
        }

        public void SetAnimationCopyWeight(float wieght)
        {
            animCopyWeight = wieght;
        }
    }
}