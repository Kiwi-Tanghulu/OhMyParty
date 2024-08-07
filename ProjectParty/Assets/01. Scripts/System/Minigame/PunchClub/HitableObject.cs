using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.PunchClub
{
    public class HitableObject : MonoBehaviour, IDamageable
    {
        [SerializeField] UnityEvent<Vector3> onHitEvent = null;
        private NetworkEvent<Vector3Params, Vector3> onHitNetworkEvent = null;

        public void Init(NetworkObject owner)
        {
            onHitNetworkEvent = new NetworkEvent<Vector3Params, Vector3>($"{gameObject.name}HitEvent");
            onHitNetworkEvent.AddListener(HandleHit);
            onHitNetworkEvent.Register(owner);
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point, HitEffectType effectType, Vector3 normal = default, Vector3 direction = default)
        {
            onHitNetworkEvent?.Invoke(point);
        }

        private void HandleHit(Vector3 hitPosition)
        {
            onHitEvent?.Invoke(hitPosition);
        }
    }
}
