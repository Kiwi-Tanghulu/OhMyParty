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
            if (!IsHost)
                return;

            if (other.CompareTag("Player"))
            {
                if(other.TryGetComponent<BikeRacePlayerController>(out BikeRacePlayerController player))
                {
                    player.Goal();
                    bikeRace.GoalPlayer(player);
                }
            }
        }
    }
}