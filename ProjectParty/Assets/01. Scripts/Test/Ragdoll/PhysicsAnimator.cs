using System;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class PhysicsAnimator : MonoBehaviour
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
        [Range(0f, 1f)]
        [SerializeField] private float animCopyWeight = 1f;

        private CopyMotion[] copyMotions;

        private ExtendedAnimator animator;
        public ExtendedAnimator Animator => animator;

        private void Awake()
        {
            copyMotions = GetComponentsInChildren<CopyMotion>();
            animator = animRoot.GetComponent<ExtendedAnimator>();

            for (int i = 0; i < copyMotions.Length; i++)
                copyMotions[i].Init(ragdollRoot, animRoot, ragdollCopyWeight, animCopyWeight);
        }

        public void SetRagdollCopyWeight(float weight)
        {
            ragdollCopyWeight = weight;

            for (int i = 0; i < copyMotions.Length; i++)
                copyMotions[i].SetRagdollCopyWeight(ragdollCopyWeight);
        }

        public void SetAnimationCopyWeight(float weight)
        {
            animCopyWeight = weight;

            for (int i = 0; i < copyMotions.Length; i++)
                copyMotions[i].SetAnimationCopyWeight(animCopyWeight);
        }
    }
}