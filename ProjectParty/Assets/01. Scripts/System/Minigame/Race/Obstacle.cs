using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.Race
{
    public class Obstacle : NetworkBehaviour
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
                other.GetComponent<ObstacleRunPlayerController>().HangingObstacle(this);
            }
        }

        public void FallDown()
        {
            anim.SetTrigger("fallDown");
            col.enabled = false;
        }
    }
}