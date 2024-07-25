using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class FinishLine : MonoBehaviour
    {
        private RaceCycle raceCycle;

        private void Start()
        {
            raceCycle = MinigameManager.Instance.CurrentMinigame.Cycle as RaceCycle;  
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                Goal(player.OwnerClientId);
            }
        }

        private void Goal(ulong playerID)
        {
            raceCycle.SetPlayerGoal(playerID);
        }
    }
}