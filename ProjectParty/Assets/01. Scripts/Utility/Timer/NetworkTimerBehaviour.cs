using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Timers
{
    [RequireComponent(typeof(NetworkTimer))]
    public class NetworkTimerBehaviour : NetworkBehaviour
    {
        [SerializeField] UnityEvent<float> onValueChanged = new UnityEvent<float>();
        [SerializeField] UnityEvent onTimerFinished = new UnityEvent();

        private NetworkVariable<float> timerValue = new NetworkVariable<float>();

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
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            timer.OnValueChangedEvent.RemoveListener(HandleValueChanged);
            timer.OnTimerFinishedEvent.RemoveListener(HandleTimerFinished);
            timerValue.OnValueChanged -= BroadcastTimerValueChanged;
        }

        private void HandleTimerFinished()
        {
            onTimerFinished?.Invoke();
        }

        private void HandleValueChanged(float value)
        {
            if(IsHost == false)
                return;

            timerValue.Value = value;
        }

        private void BroadcastTimerValueChanged(float prevValue, float newValue)
        {
            onValueChanged?.Invoke(newValue);
        }
    }
}
