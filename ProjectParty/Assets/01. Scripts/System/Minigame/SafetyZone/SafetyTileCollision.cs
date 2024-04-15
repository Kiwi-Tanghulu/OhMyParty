using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileCollision : NetworkBehaviour
    {
        private HashSet<ulong> includePlayers = null;
        public int IncludePlayerCount => includePlayers.Count;

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
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player") == false)
                return;
            
            if(other.TryGetComponent<NetworkObject>(out NetworkObject playerObject) == false)
                return;

            includePlayers.Remove(playerObject.OwnerClientId);
        }
    }
}
