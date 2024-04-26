using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.BikeRace
{
    public class RaceFinishLine : NetworkBehaviour
    {
        private Race bikeRace;

        private void Start()
        {
            bikeRace = MinigameManager.Instance.CurrentMinigame as Race;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if(other.TryGetComponent<BikeRacePlayerController>(out BikeRacePlayerController player))
                {
                    if(IsHost)
                        bikeRace.GoalPlayer(player);
                }
            }
        }
    }
}