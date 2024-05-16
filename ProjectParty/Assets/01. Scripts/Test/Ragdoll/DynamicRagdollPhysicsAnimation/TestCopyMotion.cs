using OMG.Extensions;
using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCopyMotion : MonoBehaviour
{
    private TestRagdollCopy rag;
    private AnimationCopyPart anim;

    private float ragdollWeight;

    public void Init(TestRagdollCopy rag, AnimationCopyPart anim, float ragdollWeight)
    {
        this.rag = rag;
        this.anim = anim;
        this.ragdollWeight = ragdollWeight;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(anim.GetCopyPosition(1f), rag.GetCopyPosition(1f), ragdollWeight);
        transform.localRotation = Quaternion.Lerp(anim.GetCopyRotation(1f), rag.GetCopyRotation(1f), ragdollWeight);
    }
}
