using OMG.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class RunAwayMonster : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private RunAwayCycle runAwayCycle;

        public void Init(RunAwayCycle cycle)
        {
            runAwayCycle = cycle;
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if(other.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    runAwayCycle.SetPlayerDead(player.OwnerClientId);
                }
            }
        }

        private void Move()
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}