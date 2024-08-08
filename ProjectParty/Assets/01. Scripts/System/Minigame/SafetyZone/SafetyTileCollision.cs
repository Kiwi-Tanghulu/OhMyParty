using System;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileCollision : MonoBehaviour
    {
        public event Action<SafetyZonePlayerController> OnPlayerEnterEvent = null;
        public event Action<SafetyZonePlayerController> OnPlayerExitEvent = null;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") == false && other.CompareTag("OtherPlayer") == false)
                return;
            
            if(other.TryGetComponent<SafetyZonePlayerController>(out SafetyZonePlayerController playerObject) == false)
                return;

            OnPlayerEnterEvent?.Invoke(playerObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player") == false && other.CompareTag("OtherPlayer") == false)
                return;
            
            if(other.TryGetComponent<SafetyZonePlayerController>(out SafetyZonePlayerController playerObject) == false)
                return;

            OnPlayerExitEvent?.Invoke(playerObject);
        }
    }
}
