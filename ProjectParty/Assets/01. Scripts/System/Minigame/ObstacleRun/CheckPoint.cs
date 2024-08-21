using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class CheckPoint : MonoBehaviour
    {
        public UnityEvent<Transform> OnCheckEvent; 

        [SerializeField] private Transform respawnPoint;
        private bool isChecked;

        private void OnTriggerEnter(Collider other)
        {
            if (isChecked == true)
                return;

            if(other.CompareTag("Player"))
            {
                if (other.TryGetComponent<ObstacleRunPlayerController>(out ObstacleRunPlayerController player))
                {
                    player.SetRespawnPoint(respawnPoint);
                    OnCheckEvent?.Invoke(player.FeedbackPlayPoint);
                    isChecked = true;
                }
            }
        }
    }
}