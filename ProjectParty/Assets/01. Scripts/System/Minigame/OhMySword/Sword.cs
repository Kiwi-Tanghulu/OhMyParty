using DG.Tweening;
using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] Transform bladeTransform = null;
        [SerializeField] Vector3 bladeDirection = Vector3.up;
        [SerializeField] float scaleFactor = 0.02f;
        [SerializeField] float growSpeed = 3f;

        private NetworkObject owner = null;
        private BoxCollider bladeCollider = null;
        private bool collisionActive = false;
        private float currentLength = 0f;
        private float length = 0f;
        private bool active = false;

        private void Awake()
        {
            bladeCollider = GetComponent<BoxCollider>();
        }

        public void Init(NetworkObject owner)
        {
            active = owner.IsOwner;
            this.owner = owner;
        }

        private void FixedUpdate()
        {
            if(currentLength >= length)
                return;

            currentLength += Time.fixedDeltaTime * growSpeed;
            currentLength = Mathf.Min(currentLength, length);

            bladeTransform.localScale = Vector3.one + bladeDirection * currentLength;
            ResizeCollider(currentLength + 1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(active == false)
                return;
            if(collisionActive == false)
                return;

            if(other.TryGetComponent<IDamageable>(out IDamageable id) == false)
                return;

            id?.OnDamaged(5000, owner.transform, other.transform.position, HitEffectType.Die);
        }

        public void SetLength(float length)
        {
            this.length = length * scaleFactor;
        }

        public void ResetLength()
        {
            currentLength = 0;
        }

        /// <summary>
        /// only owner could call this method
        /// </summary>
        public void ActiveCollision(bool active)
        {
            collisionActive = active;
        }

        public void ToggleCollisionActive()
        {
            ActiveCollision(!collisionActive);
        }

        private void ResizeCollider(float length)
        {
            Vector3 center = bladeCollider.center;
            center.y = GetColliderOffset(length);
            bladeCollider.center = center;

            Vector3 size = bladeCollider.size;
            size.y = GetColliderSize(length);
            bladeCollider.size = size;
        }

        private float GetColliderOffset(float length) => MathExtensions.ArithmeticSequence(length, 0.9f, 0.8f);
        private float GetColliderSize(float length) => MathExtensions.ArithmeticSequence(length, 1.5f, 1.6f);
    }
}
