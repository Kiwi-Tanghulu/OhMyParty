using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerRagdoll : MonoBehaviour
    {
        [SerializeField] private Transform hipTrm;
        private List<Rigidbody> rbList;
        private List<Collider> colList;
        private Rigidbody hipRb;
        private Animator anim;
        private PlayerMovement movement;

        public Transform HipTrm => hipTrm;
        public Rigidbody HipRb => hipRb;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            movement = transform.parent.GetComponent<PlayerMovement>();
            hipRb = hipTrm.GetComponent<Rigidbody>();

            rbList = new List<Rigidbody>();
            GetRagdollCompo(hipTrm);
        }

        public void SetActive(bool value)
        {
            anim.enabled = !value;
            movement.Collider.enabled = !value;
            movement.Rigidbody.isKinematic = value;

            for(int i = 0; i < rbList.Count; i++)
            {
                rbList[i].isKinematic = !value;
                if (value)
                    rbList[i].velocity = Vector3.zero;
            }
            for (int i = 0; i < rbList.Count; i++)
            {
                colList[i].enabled = value;
            }
        }

        public void AddForce(Vector3 power, ForceMode mode)
        {
            hipRb.AddForce(power, mode);
        }

        public void AddForceAtPosition(Vector3 power, Vector3 point, ForceMode mode)
        {
            hipRb.AddForceAtPosition(power, point, mode);
        }

        private void GetRagdollCompo(Transform trm)
        {
            if(trm.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rbList.Add(rb);
            if (trm.TryGetComponent<Collider>(out Collider col))
                colList.Add(col);

            for (int i = 0; i < trm.childCount; i++)
            {
                GetRagdollCompo(trm.GetChild(i));
            }
        }
    }
}
