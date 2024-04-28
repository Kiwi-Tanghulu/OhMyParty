using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.Race
{
    public class RaceFinishLine : NetworkBehaviour
    {
        private Race Race;

        private void Start()
        {
            Race = MinigameManager.Instance.CurrentMinigame as Race;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsHost)
                return;

            if (other.CompareTag("Player"))
            {
                if(other.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    
                    Race.GoalPlayer(player);
                }
            }
        }
    }
}