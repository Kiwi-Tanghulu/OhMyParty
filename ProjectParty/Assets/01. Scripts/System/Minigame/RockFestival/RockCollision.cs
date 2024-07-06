using OMG.Utility.Netcodes;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.RockFestival
{
    public class RockCollision : NetworkBehaviour, IDamageable
    {
        [SerializeField] UnityEvent onCollisionEvent = null;
        [SerializeField] UnityEvent onDamagedEvent = null;
        [SerializeField] Transform hitPoint = null;

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
            onCollisionEvent?.Invoke();

            if(ActiveCollisionOther == false)
                return;

            if(other.collider.CompareTag("Ground")) // 땅에 닿으면 종료
            {
                SetActiveCollisionOther(false);
                return;
            }

            if(other.rigidbody == null)
                return;

            if(other.rigidbody.TryGetComponent<IDamageable>(out IDamageable damageable) == false)
                return;

            damageable?.OnDamaged(rigidbody.velocity.magnitude, transform, other.contacts[0].point, HitEffectType.None, other.contacts[0].normal);
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point,
        HitEffectType effectType, Vector3 normal = default)
        {
            // Calc Normal Vector
            if(ActiveCollisionOther)
                return;

            OwnershipController otherRockOwner = attacker.GetComponent<OwnershipController>();
            if (otherRockOwner == null)
                return;

            otherRockOwner.SetOwner(OwnerClientId, 
            () => { // Owner Action
                normal = attacker.position - transform.position;
                normal = new Vector3(normal.x, 0, normal.z).normalized;
                normal.y = -0.25f;

                rigidbody.AddForce(-normal.normalized * damage * 100f);
            }, 
            () => { // Broadcast Action
                hitPoint.position = point;
                onDamagedEvent?.Invoke();
            });
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
