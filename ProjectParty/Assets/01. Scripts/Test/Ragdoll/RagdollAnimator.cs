using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Ragdoll
{
    public class RagdollAnimator : MonoBehaviour
    {
        [SerializeField] private Transform ragdollRootTrm;
        private List<CopyMotion> copyMotionList;

        private float weight;
        public float Weight => weight;

        private void Awake()
        {
            copyMotionList = new List<CopyMotion>();

            GetCopyMotionCompo(ragdollRootTrm);

            SetCopyMotionWeight(1f);
        }

        public void SetCopyMotionWeight(float value)
        {
            value = Mathf.Clamp(value, 0f, 1f);

            weight = value;

            for(int i = 0; i <  copyMotionList.Count; i++)
            {
                copyMotionList[i].SetCopyMotionWeight(weight);
            }
        }
        
        private void GetCopyMotionCompo(Transform trm)
        {
            foreach (Transform child in trm)
            {
                if(child.TryGetComponent<CopyMotion>(out CopyMotion compo))
                {
                    copyMotionList.Add(compo);
                }

                GetCopyMotionCompo(child);
            }
        }
    }
}