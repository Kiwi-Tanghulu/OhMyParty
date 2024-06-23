using System;
using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Test
{
    public class TNetworkEvent : NetworkBehaviour
    {
        [SerializeField] NetworkEvent<IntParams> testEvent = new NetworkEvent<IntParams>("TestEvent");

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            testEvent.Register(GetComponent<NetworkObject>());
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                IntParams asd = new IntParams();
                asd.Value = DateTime.Now.Millisecond;
                testEvent.Broadcast(asd);
            }
            
            // if(Input.GetKeyDown(KeyCode.Space))
            // {
            //     BroadcastServerRpc(DateTime.Now.Millisecond);
            // }   
        }

        public void HandleTestEvent(IntParams eventParams)
        {
            Debug.Log($"ping : {DateTime.Now.Millisecond - eventParams.Value}ms");
            // Debug.Log(eventParams.Value);
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
