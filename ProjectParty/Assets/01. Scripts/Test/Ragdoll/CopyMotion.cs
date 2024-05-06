using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform CopyTrm;
    public bool IsRagdoll;
    [Range(0f, 1f)]
    public float Weight = 1f;

    private ConfigurableJoint joint;

    private Vector3 MirrorAnchorPosition;
    private Quaternion MirrorAnchorRotation;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();

        Weight = 1f;

        if (CopyTrm.name != transform.name)
            Debug.Log($"wrong sync object : {transform.name}");
    }

    private void Start()
    {
        MirrorAnchorPosition = CopyTrm.position;
        MirrorAnchorRotation = CopyTrm.rotation;
    }

    private void Update()
    {
        if(IsRagdoll)
        {
            Vector3 MirrorTargetPosition = GetTargetPosition(CopyTrm.transform.position, MirrorAnchorPosition);
            joint.targetPosition = MirrorTargetPosition * Weight;

            Quaternion MirrorTargetRotation = GetTargetRotation(CopyTrm.transform.rotation, MirrorAnchorRotation);
            joint.targetRotation = Quaternion.Lerp(Quaternion.identity, MirrorTargetRotation, Weight);
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
