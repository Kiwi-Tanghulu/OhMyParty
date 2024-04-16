using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class DeadZone : NetworkBehaviour
    {
        [SerializeField] DeathmatchCycle cycle = null;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") == false)
                return;
            
            if(other.TryGetComponent<NetworkObject>(out NetworkObject playerObject) == false)
                return;

            cycle.HandlePlayerDead(playerObject.OwnerClientId);
        }
    }
}
