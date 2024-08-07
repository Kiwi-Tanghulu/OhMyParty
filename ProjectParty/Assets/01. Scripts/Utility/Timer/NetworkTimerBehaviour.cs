using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Timers
{
    public class NetworkTimerBehaviour : NetworkBehaviour
    {
        /// <summary>
        /// (ratio, single)
        /// </summary>
        [SerializeField] UnityEvent<float, float> onValueChanged = new UnityEvent<float, float>();
        [SerializeField] NetworkEvents.NetworkEvent onTimerFinished = new NetworkEvents.NetworkEvent("TimerFinished");

        private NetworkVariable<TimerValue> timerValue = new NetworkVariable<TimerValue>();

        private NetworkTimer timer = null;

        private void Awake()
        {
            timer = GetComponent<NetworkTimer>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            timer.OnValueChangedEvent.AddListener(HandleValueChanged);
            timer.OnTimerFinishedEvent.AddListener(HandleTimerFinished);
            timerValue.OnValueChanged += BroadcastTimerValueChanged;

            onTimerFinished.Register(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            timer.OnValueChangedEvent.RemoveListener(HandleValueChanged);
            timer.OnTimerFinishedEvent.RemoveListener(HandleTimerFinished);
            timerValue.OnValueChanged -= BroadcastTimerValueChanged;

            onTimerFinished.Unregister();
        }

        private void HandleTimerFinished()
        {
            onTimerFinished?.Broadcast(false);
        }

        private void HandleValueChanged(float ratio, float single)
        {
            if(IsHost == false)
                return;

            timerValue.Value = new TimerValue(ratio, single);
        }

        private void BroadcastTimerValueChanged(TimerValue prevValue, TimerValue newValue)
        {
            onValueChanged?.Invoke(newValue.ratio, newValue.single);
        }
    }
}
