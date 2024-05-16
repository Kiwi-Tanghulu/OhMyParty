using OMG.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class CopyMotion : MonoBehaviour
    {
        private CopyPart ragdollCopyPart;
        private CopyPart animCopyPart;

        private float ragdollCopyWeight;
        private float animCopyWeight;

        public void Init(Transform ragdollRoot, Transform animRoot, float ragdollCopyWeight, float animCopyWeight)
        {
            ragdollCopyPart = ragdollRoot.FindFromAll(transform.name).GetComponent<CopyPart>();
            animCopyPart = animRoot.FindFromAll(transform.name).GetComponent<CopyPart>();

            ragdollCopyPart.Init(animRoot, ragdollRoot);
            animCopyPart.Init(animRoot, ragdollRoot);

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
            Vector3 copyPos = animCopyPart.GetCopyPosition(animCopyWeight) /*+ ragdollCopyPart.GetCopyPosition(ragdollCopyWeight)*/;
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