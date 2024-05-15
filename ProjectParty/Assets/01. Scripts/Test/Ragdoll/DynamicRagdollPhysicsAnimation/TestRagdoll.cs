using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestRagdoll : MonoBehaviour
{
    List<TestRagdollCopy> rp;
    List<AnimationCopyPart> ap;
    List<TestCopyMotion> cp;
    public Transform RagdollCopyTargetRoot;
    public Transform AnimCopyTargetRoot;

    [Range(0f, 1f)]
    [SerializeField] private float ragdollWeight;

    private void Awake()
    {
        rp = RagdollCopyTargetRoot.GetComponentsInChildren<TestRagdollCopy>().ToList();
        ap = AnimCopyTargetRoot.GetComponentsInChildren<AnimationCopyPart>().ToList();
        cp = GetComponentsInChildren<TestCopyMotion>().ToList();

        foreach (TestRagdollCopy ragdoll in rp)
            ragdoll.Init(AnimCopyTargetRoot);
        foreach (AnimationCopyPart anim in ap)
            anim.Init(AnimCopyTargetRoot);
        foreach (TestCopyMotion c in cp)
            c.Init(rp.Find(x => x.name == c.name), ap.Find(x => x.name == c.name), ragdollWeight);
    }
}
