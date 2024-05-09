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

        [SerializeField] private RagdollPart ragdollCopyPart;
        [SerializeField] private Transform animCopyTrm;

        [SerializeField] private float ragdollCopyWeight;

        public void Init(Transform ragdollRoot, Transform animCopyRoot, float ragdollCopyWeight)
        {
            ragdollCopyPart = ragdollRoot.FindFromAll(transform.name).GetComponent<RagdollPart>();
            animCopyTrm = animCopyRoot.FindFromAll(transform.name);

            ragdollCopyPart.Init(ragdollRoot);

            SetRagdollCopyWeight(ragdollCopyWeight);
        }

        public void Update()
        {
            if(animCopyTrm == null || ragdollCopyPart == null)
            {
                Debug.Log($"not setting copy motion component : {transform.name}");
                return;
            }

            Vector3 copyPos = animCopyTrm.localPosition + ragdollCopyPart.GetCopyPos(ragdollCopyWeight);
            Quaternion copyRot = ragdollCopyPart.GetCopyRot(ragdollCopyWeight) * animCopyTrm.localRotation;

            transform.localPosition = copyPos;
            transform.localRotation = copyRot;
        }

        public void SetRagdollCopyWeight(float wieght)
        {
            ragdollCopyWeight = wieght;
        }
    }
}