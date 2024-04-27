using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.Race
{
    public class Obstacle : MonoBehaviour
    {
        private Animator anim;
        private Collider col;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FallDown();
            }
        }

        private void FallDown()
        {
            anim.SetTrigger("fallDown");
            col.enabled = false;
        }
    }
}