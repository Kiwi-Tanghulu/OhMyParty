using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class RagdollPart : MonoBehaviour
    {
        

        //private ConfigurableJoint joint;

        

        //public void Init(Transform root)
        //{
        //    joint = GetComponent<ConfigurableJoint>();
        //}

        //private void FixedUpdate()
        //{
        //    if (root == null)
        //        return;

        //    Vector3 targetPosition = -root.position;

        //    joint.targetPosition = targetPosition;
        //}

        //public Vector3 GetCopyPos(float weight)
        //{
        //    weight = Mathf.Clamp(weight, 0f, 1f);

        //    return (originPos - transform.localPosition) * weight;
        //}

        //public Quaternion GetCopyRot(float weight)
        //{
        //    weight = Mathf.Clamp(weight, 0f, 1f);

        //    return Quaternion.Lerp(Quaternion.Euler(Vector3.zero),
        //        Quaternion.Inverse(Quaternion.Inverse(transform.localRotation) * originRot), weight);
        //}
    }
}