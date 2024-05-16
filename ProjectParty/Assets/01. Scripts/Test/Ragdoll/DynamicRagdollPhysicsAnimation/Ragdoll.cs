using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private List<RagdollCopyPart> rp;
    private List<AnimationCopyPart> ap;
    private List<TestCopyMotion> cp;

    public Transform RagdollCopyTargetRoot;
    public Transform AnimCopyTargetRoot;

    [Range(0f, 1f)]
    [SerializeField] private float ragdollWeight;

    private void Awake()
    {
        rp = RagdollCopyTargetRoot.GetComponentsInChildren<RagdollCopyPart>().ToList();
        ap = AnimCopyTargetRoot.GetComponentsInChildren<AnimationCopyPart>().ToList();
        cp = GetComponentsInChildren<TestCopyMotion>().ToList();

        foreach (RagdollCopyPart ragdoll in rp)
            ragdoll.Init(AnimCopyTargetRoot, RagdollCopyTargetRoot);
        foreach (AnimationCopyPart anim in ap)
            anim.Init(AnimCopyTargetRoot, RagdollCopyTargetRoot);
        foreach (TestCopyMotion c in cp)
            c.Init(rp.Find(x => x.name == c.name), ap.Find(x => x.name == c.name), ragdollWeight);
    }

    private void Update()
    {
        for(int i = 0; i < cp.Count; i++)
            cp[i].SetRagdollWeight(ragdollWeight);
    }
}
