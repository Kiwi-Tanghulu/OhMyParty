using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerRagdoll : MonoBehaviour
    {
        [SerializeField] private Transform hipTrm;
        private Animator anim;
        private List<Rigidbody> rbList;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            rbList = new List<Rigidbody>();
            GetRigidbody(hipTrm);
        }

        public void SetActive(bool value)
        {
            anim.enabled = !value;

            for(int i = 0; i < rbList.Count; i++)
            {
                rbList[i].isKinematic = !value;
                if (value)
                    rbList[i].velocity = Vector3.zero;
            }
        }

        private void GetRigidbody(Transform trm)
        {
            if(trm.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rbList.Add(rb);

            for(int i = 0; i < trm.childCount; i++)
            {
                GetRigidbody(trm.GetChild(i));
            }
        }
    }
}
