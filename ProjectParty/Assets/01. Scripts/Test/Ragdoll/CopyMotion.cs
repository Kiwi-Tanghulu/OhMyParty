using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform CopyTrm;
    public bool IsRagdoll;
    private ConfigurableJoint joint;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Start()
    {
        if (gameObject.name != CopyTrm.name)
            Debug.Log(123);
    }

    private void Update()
    {
        if (IsRagdoll)
            joint.targetRotation = CopyTrm.rotation;
        else
            transform.localRotation = CopyTrm.localRotation;
    }
}
