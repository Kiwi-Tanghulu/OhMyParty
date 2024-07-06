using Unity.Netcode;
using UnityEngine;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Minigames.OhMySword
{
    public class XPObject : NetworkBehaviour
    {
        [SerializeField] int xpAmount = 10;
        [SerializeField] NetworkEvent onCollectedEvent = new NetworkEvent("Collected");

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            onCollectedEvent.AddListener(HandleCollected);
            onCollectedEvent.Register(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            onCollectedEvent.Unregister();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") == false)
                return;

            if(other.TryGetComponent<OhMySwordPlayerController>(out OhMySwordPlayerController player) == false)
                return;

            if(player.IsOwner == false)
                return;

            player.GetXP(xpAmount);
            onCollectedEvent?.Broadcast();
        }

        public void Init(Vector3 position)
        {
            // bounce to position
            transform.position = position;
        }

        private void HandleCollected(NetworkEvents.NoneParams unused)
        {
            if(IsHost)
                NetworkObject.Despawn();
        }
    }
}
