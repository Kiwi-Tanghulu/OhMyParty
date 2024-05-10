using OMG.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class RagdollAnimator : MonoBehaviour
    {
        #region
        //[SerializeField] private Transform ragdollRootTrm;
        //private List<CopyMotion> copyMotionList;

        //private float weight;
        //public float Weight => weight;

        //private void Awake()
        //{
        //    copyMotionList = new List<CopyMotion>();

        //    GetCopyMotionCompo(ragdollRootTrm);

        //    SetCopyMotionWeight(1f);
        //}

        //public void SetCopyMotionWeight(float value)
        //{
        //    value = Mathf.Clamp(value, 0f, 1f);

        //    weight = value;

        //    //for(int i = 0; i <  copyMotionList.Count; i++)
        //    //{
        //    //    copyMotionList[i].SetCopyMotionWeight(weight);
        //    //}
        //}

        //private void GetCopyMotionCompo(Transform trm)
        //{
        //    foreach (Transform child in trm)
        //    {
        //        if(child.TryGetComponent<CopyMotion>(out CopyMotion compo))
        //        {
        //            copyMotionList.Add(compo);
        //        }

        //        GetCopyMotionCompo(child);
        //    }
        //}
        #endregion
        [SerializeField] private Transform ragdollRoot;
        [SerializeField] private Transform animRoot;

        [Space]
        [Range(0f, 1f)]
        [SerializeField] private float ragdollCopyWeight = 1f;

        private CopyMotion[] copyMotions;

        private Animator anim;

        private void Awake()
        {
            copyMotions = GetComponentsInChildren<CopyMotion>();
            anim = animRoot.GetComponent<Animator>();

            for (int i = 0; i < copyMotions.Length; i++)
                copyMotions[i].Init(ragdollRoot, animRoot, ragdollCopyWeight);
        }

        public void SetRagdollCopyWeight(float weight)
        {
            for (int i = 0; i < copyMotions.Length; i++)
                copyMotions[i].SetRagdollCopyWeight(weight);
        }
    }
}