using Unity.Netcode;
using UnityEngine;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Test
{
    public class TNetworkEvent : NetworkBehaviour
    {
        [SerializeField] NetworkEvent testEvent = new NetworkEvent("TestEvent");

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            testEvent.Register(NetworkObject);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                testEvent.Broadcast();
        }

        public void HandleTestEvent()
        {
            Debug.Log("Handle Test Event");
        }
    }
}
