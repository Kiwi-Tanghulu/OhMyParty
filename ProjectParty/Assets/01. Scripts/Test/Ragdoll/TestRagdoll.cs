using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestRagdoll : MonoBehaviour
{
    List<TestCopyMotion> cp;
    public Transform CopyTargetRoot;

    private void Awake()
    {
        cp = GetComponentsInChildren<TestCopyMotion>().ToList();

        foreach (TestCopyMotion motion in cp)
        {
            motion.Init(CopyTargetRoot);
        }
    }
}
