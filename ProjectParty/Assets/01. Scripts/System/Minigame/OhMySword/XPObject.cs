using DG.Tweening;
using OMG.NetworkEvents;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Minigames.OhMySword
{
    public class XPObject : NetworkBehaviour
    {
        [SerializeField] int xpAmount = 10;
            
        [Space(10f)]
        [SerializeField] float jumpPower = 1.5f;
        [SerializeField] float jumpDuration = 1f;
        [SerializeField] float floatDistance = 1f;
        [SerializeField] LayerMask groundLayer = 1 << 6;
        [SerializeField] AnimationCurve easeCurve;
        [SerializeField] TrailRenderer trail;

        [Space(15f)]
        [SerializeField] NetworkEvent onCollectedEvent = new NetworkEvent("Collected");
        [SerializeField] NetworkEvent<Vector3Params> onInitEvent = new NetworkEvent<Vector3Params>("Init");

        private bool active = false;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        
            onCollectedEvent.AddListener(HandleCollected);
            onCollectedEvent.Register(NetworkObject);

            onInitEvent.AddListener(HandleInit);
            onInitEvent.Register(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            onCollectedEvent.Unregister();
            onInitEvent.Unregister();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(active == false)
                return;

            if(other.CompareTag("Player") == false)
                return;

            if(other.TryGetComponent<OhMySwordPlayerController>(out OhMySwordPlayerController player) == false)
                return;

            if(player.IsOwner == false)
                return;

            player.GetXP(xpAmount);
            onCollectedEvent?.Broadcast(false);
        }

        public void Init(Vector3 position)
        {
            onInitEvent?.Broadcast(position);
        }

        private void HandleInit(Vector3Params position)
        {
            if(Physics.Raycast(position, Vector3.down, out RaycastHit hit, float.MaxValue, groundLayer))
                position.Value.y = hit.point.y + floatDistance;

            float factor = 1f + (Mathf.Log10(xpAmount) + 1) * 0.5f;
            transform.DOJump(position, jumpPower * factor, 1, jumpDuration)
                .SetEase(easeCurve)
                .OnComplete(() => {
                    trail.enabled = false;
                    active = true;
                }
            ).Play();
        }

        private void HandleCollected(NoneParams unused)
        {
            if(IsHost)
                NetworkObject.Despawn();
        }
    }
}
