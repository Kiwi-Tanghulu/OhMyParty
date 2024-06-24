using System;
using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Test
{
    public class TNetworkEvent : NetworkBehaviour
    {
        [SerializeField] NetworkEvents.NetworkEvent testEvent = new NetworkEvents.NetworkEvent("TestEvent");

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            testEvent.Register(GetComponent<NetworkObject>());
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                testEvent.Broadcast();
            }
            
            // if(Input.GetKeyDown(KeyCode.Space))
            // {
            //     BroadcastServerRpc(DateTime.Now.Millisecond);
            // }   
        }

        public void HandleTestEvent()
        {
            // Debug.Log($"ping : {DateTime.Now.Millisecond - eventParams.Value}ms");
            // Debug.Log(eventParams.Value);
            Debug.Log("Handle Test Event");
        }

        [ServerRpc]
        private void BroadcastServerRpc(int value)
        {
            BroadcastClientRpc(value);
        }

        [ClientRpc]
        private void BroadcastClientRpc(int value)
        {
            Debug.Log($"ping : {DateTime.Now.Millisecond - value}ms");
        }
    }
}
