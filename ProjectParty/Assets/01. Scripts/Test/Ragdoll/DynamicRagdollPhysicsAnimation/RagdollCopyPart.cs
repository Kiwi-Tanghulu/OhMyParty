using OMG.Extensions;
using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCopyPart : CopyPart
{
    private Transform copyTarget;
    private ConfigurableJoint joint;

    public bool IsHip;

    public override void Init(Transform animRoot, Transform ragdollRoot)
    {
        base.Init(animRoot, ragdollRoot);    

        copyTarget = animRoot.FindFromAll(transform.name);
        joint = GetComponent<ConfigurableJoint>();

        if (IsHip)
        {
            originPos = joint.connectedBody.transform.localPosition;
        }
            
    }

    private void FixedUpdate()
    {
        if (copyTarget == null)
            return;

        //joint.targetPosition = copyTarget.localPosition - originPos;
        joint.targetRotation = Quaternion.Inverse(copyTarget.localRotation) * originRot;
    }

    public override Vector3 GetCopyPosition(float weight)
    {
        Vector3 copyPos = IsHip ? transform.localPosition + originPos : transform.localPosition;

        return Vector3.Lerp(originPos, copyPos, weight);
    }

    public override Quaternion GetCopyRotation(float weight)
    {
        return Quaternion.Lerp(originRot, transform.localRotation, weight);
    }
}