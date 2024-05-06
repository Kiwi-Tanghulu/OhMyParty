using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform CopyTrm;
    public bool IsRagdoll;
    private ConfigurableJoint joint;

    private Vector3 MirrorAnchorPosition;
    private Quaternion MirrorAnchorRotation;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Start()
    {
        if (gameObject.name != CopyTrm.name)
            Debug.Log(123);

        MirrorAnchorRotation = CopyTrm.rotation;
    }

    private void Update()
    {
        if(IsRagdoll)
        {
            Vector3 MirrorTargetPosition = GetTargetPosition(CopyTrm.transform.position, MirrorAnchorPosition);
            joint.targetPosition = MirrorTargetPosition;

            Quaternion MirrorTargetRotation = GetTargetRotation(CopyTrm.transform.rotation, MirrorAnchorRotation);
            joint.targetRotation = MirrorTargetRotation;
        }
        else
        {
            transform.localRotation = CopyTrm.localRotation;
        }
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
