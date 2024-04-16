using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileCollision : MonoBehaviour
    {
        private HashSet<ulong> includePlayers = null;
        public int IncludePlayerCount => includePlayers.Count;

        public event Action OnPlayerCountChangedEvent = null;

        private void Awake()
        {
            includePlayers = new HashSet<ulong>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") == false)
                return;
            
            if(other.TryGetComponent<NetworkObject>(out NetworkObject playerObject) == false)
                return;

            includePlayers.Add(playerObject.OwnerClientId);
            OnPlayerCountChangedEvent?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player") == false)
                return;
            
            if(other.TryGetComponent<NetworkObject>(out NetworkObject playerObject) == false)
                return;

            includePlayers.Remove(playerObject.OwnerClientId);
            OnPlayerCountChangedEvent?.Invoke();
        }
    }
}
