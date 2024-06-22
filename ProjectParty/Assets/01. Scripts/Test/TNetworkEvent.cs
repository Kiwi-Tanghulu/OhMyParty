using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Test
{
    public class TNetworkEvent : NetworkBehaviour
    {
        [SerializeField] NetworkEvent<StringParams> testEvent = new NetworkEvent<StringParams>("TestEvent");

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            testEvent.Register(GetComponent<NetworkObject>());
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StringParams param = new StringParams("Hi");
                testEvent.Broadcast(param);
            }
        }

        public void HandleTestEvent(StringParams eventParams)
        {
            Debug.Log(eventParams.Value);
        }
    }
}
