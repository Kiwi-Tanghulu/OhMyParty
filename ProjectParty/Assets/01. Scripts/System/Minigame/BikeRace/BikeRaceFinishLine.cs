using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.BikeRace
{
    public class BikeRaceFinishLine : NetworkBehaviour
    {
        private BikeRace bikeRace;

        private void Start()
        {
            bikeRace = MinigameManager.Instance.CurrentMinigame as BikeRace;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if (!IsHost)
                    return;

                if(other.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    bikeRace.GoalPlayer(player);
                }
            }
        }
    }
}