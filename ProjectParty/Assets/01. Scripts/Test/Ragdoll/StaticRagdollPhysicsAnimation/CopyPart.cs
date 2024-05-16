using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public abstract class CopyPart : MonoBehaviour
    {
        protected Vector3 originPos;
        protected Quaternion originRot;

        public virtual void Init(Transform animRoot, Transform ragdollRoot)
        {
            originPos = transform.localPosition;
            originRot = transform.localRotation;
        }

        public abstract Vector3 GetCopyPosition(float weight); 
        public abstract Quaternion GetCopyRotation(float weight); 
    }
}
