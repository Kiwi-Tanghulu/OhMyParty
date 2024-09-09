using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace OMG.Minigames.WhatTheFC
{
    public class SoccerBall : NetworkBehaviour, IDamageable
    {
        [SerializeField] float shootSpeed = 10f;
        [SerializeField] UnityEvent<Vector3> onHitEvent = null;

        private NetworkEvent<Vector3Params, Vector3> onHitNetworkEvent = new NetworkEvent<Vector3Params, Vector3>("BallHit");

        private Rigidbody ballRigidbody = null;

        private void Awake()
        {
            ballRigidbody = GetComponent<Rigidbody>();
        }

        public void ResetRigidbody()
        {
            ballRigidbody.angularVelocity = Vector3.zero;
            ballRigidbody.velocity = Vector3.zero;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onHitNetworkEvent.AddListener(HandleHit);
            onHitNetworkEvent.Register(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            onHitNetworkEvent.Unregister();
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point, HitEffectType effectType, Vector3 normal = default, Vector3 direction = default)
        {
            onHitNetworkEvent?.Broadcast(point, false);
        }

        private void HandleHit(Vector3 hitPosition)
        {
            onHitEvent?.Invoke(hitPosition);
            
            if(IsHost)
            {
                Vector3 direction = (transform.position - hitPosition).normalized;
                ballRigidbody.AddForce(direction * shootSpeed, ForceMode.Impulse);
            }
        }
    }
}
