using OMG.Extensions;
using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCopyMotion : MonoBehaviour
{
    private RagdollCopyPart ragdollCopyPart;
    private AnimationCopyPart animCopyPart;

    private float ragdollWeight;

    public void Init(RagdollCopyPart rag, AnimationCopyPart anim, float ragdollWeight)
    {
        this.ragdollCopyPart = rag;
        this.animCopyPart = anim;
        this.ragdollWeight = ragdollWeight;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(animCopyPart.GetCopyPosition(1f),
            ragdollCopyPart.GetCopyPosition(1f), ragdollWeight);
        transform.localRotation = Quaternion.Lerp(animCopyPart.GetCopyRotation(1f),
            ragdollCopyPart.GetCopyRotation(1f), ragdollWeight);
    }

    public void SetRagdollWeight(float value)
    {
        ragdollWeight = value;
    }
}
