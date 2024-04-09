using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockCollision : NetworkBehaviour, IDamageable
    {
        public bool ActiveCollisionOther { get; private set; }

        private new Rigidbody rigidbody = null;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public override void OnNetworkSpawn()
        {
            SetActiveCollisionOther(false);
            ActiveRigidbody(true);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(ActiveCollisionOther == false)
                return;

            if(other.rigidbody == null)
                return;

            if(other.rigidbody.TryGetComponent<IDamageable>(out IDamageable damageable))
                damageable?.OnDamaged(rigidbody.velocity.magnitude, transform, other.contacts[0].normal);
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point)
        {
            // Calc Normal Vector
            if(ActiveCollisionOther)
                return;

            Vector3 normal = point;
            rigidbody.AddForce(-normal * damage * 100f);
            Debug.Log("HIT");
            // 누군가가 날 맞췄어
            // 그럼 내 꺼에서만 애드포스 하면 되나?
        }

        public void SetActiveCollisionOther(bool active)
        {
            ActiveCollisionOther = active;
        }

        public void ActiveRigidbody(bool active)
        {
            rigidbody.useGravity = active;
            if(active)
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            else
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
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
