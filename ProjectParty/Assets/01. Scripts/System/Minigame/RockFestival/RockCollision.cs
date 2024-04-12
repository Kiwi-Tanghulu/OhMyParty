using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockCollision : NetworkBehaviour, IDamageable
    {
        [SerializeField] LayerMask groundLayer = 0;

        private new Rigidbody rigidbody = null;
        public bool ActiveCollisionOther { get; private set; } = false;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Init()
        {
            SetActiveCollisionOther(false);
            SetActiveRigidbody(true);
        }

        private void OnCollisionEnter(Collision other)
        {

            if(ActiveCollisionOther == false)
                return;

            if(other.rigidbody == null)
                return;
            
            if(other.rigidbody.CompareTag("Ground")) // 땅에 닿으면 종료
            {
                SetActiveCollisionOther(false);
                return;
            }

            if(other.rigidbody.TryGetComponent<IDamageable>(out IDamageable damageable))
                damageable?.OnDamaged(rigidbody.velocity.magnitude, transform, other.contacts[0].normal);
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point)
        {
            // Calc Normal Vector
            if(ActiveCollisionOther)
                return;

            Vector3 normal = point.normalized;
            rigidbody.AddForce(-normal * damage * 100f);
        }

        public void SetActiveCollisionOther(bool active)
        {
            ActiveCollisionOther = active;
        }

        public void SetActiveRigidbody(bool active)
        {
            rigidbody.useGravity = active;
            if(active)
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            else
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void FitToGround()
        {
            rigidbody.velocity = Vector3.zero;
            if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, float.MaxValue, groundLayer))
                transform.position = hit.point + (transform.forward * 0.5f) + (Vector3.up * 0.5f);
        }

        public void AddForce(Vector3 direction, float power)
        {
            rigidbody.AddForce(direction.normalized * power, ForceMode.Impulse);
        }

        #if UNITY_EDITOR
        [ContextMenu("Toggle Collision Other")]
        private void ToggleCollisionOther()
        {
            ActiveCollisionOther = !ActiveCollisionOther;
        }

        [ContextMenu("Add Force Forward")]
        private void AddForce()
        {
            rigidbody.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        }
        #endif
    }
}
