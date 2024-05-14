using OMG.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCopyMotion : MonoBehaviour
{
    private Transform copyTarget;
    private ConfigurableJoint joint;

    private Quaternion originRot;

    private void Awake()
    {
        originRot = transform.localRotation;
    }

    public void Init(Transform copyTargetRoot)
    {
        copyTarget = copyTargetRoot.FindFromAll(transform.name);
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if (copyTarget == null)
            return;

        joint.targetRotation = Quaternion.Inverse(copyTarget.localRotation) * originRot;
    }
}
