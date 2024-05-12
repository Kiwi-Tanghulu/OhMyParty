using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class AnimaionCopyPart : CopyPart
    {
        public override Vector3 GetCopyPosition(float weight)
        {
            return Vector3.Lerp(originPos, transform.localPosition, weight);
        }

        public override Quaternion GetCopyRotation(float weight)
        {
            return Quaternion.Lerp(originRot, transform.localRotation, weight);
        }
    }
}