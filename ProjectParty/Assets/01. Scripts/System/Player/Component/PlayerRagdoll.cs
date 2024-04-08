using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerRagdoll : MonoBehaviour
    {
        [SerializeField] private Transform hipTrm;
        private List<Rigidbody> rbList;
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
            GetRigidbody(hipTrm);
        }

        public void SetActive(bool value)
        {
            anim.enabled = !value;
            movement.Controller.enabled = !value;

            for(int i = 0; i < rbList.Count; i++)
            {
                rbList[i].isKinematic = !value;
                if (value)
                    rbList[i].velocity = Vector3.zero;
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
