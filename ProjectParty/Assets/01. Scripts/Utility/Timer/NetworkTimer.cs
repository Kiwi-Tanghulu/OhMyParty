using Unity.Netcode;
using UnityEngine;

namespace OMG.Timers
{
    [RequireComponent(typeof(NetworkTimerBehaviour))]
    public class NetworkTimer : Timer
    {
        private NetworkTimerBehaviour behaviour = null;

        public NetworkObject NetworkObject => behaviour.NetworkObject;
        public bool IsHost => behaviour.IsHost;

        private void Awake()
        {
            behaviour = GetComponent<NetworkTimerBehaviour>();
        }

        protected override void Update()
        {
            if(IsHost == false)
                return;
            
            base.Update();
        }
    }
}
