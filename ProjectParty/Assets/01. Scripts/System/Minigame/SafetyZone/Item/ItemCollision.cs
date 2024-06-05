using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.SafetyZone
{
    public class ItemCollision : MonoBehaviour
    {
        public UnityEvent<Collision> OnCollisionEvent = new UnityEvent<Collision>();

        private new Rigidbody rigidbody = null;
        public bool ActiveCollisionOther { get; private set; } = false;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Init()
        {
            SetActiveCollisionOther(false);
            SetActiveRigidbody(false);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(ActiveCollisionOther == false)
                return;

            OnCollisionEvent?.Invoke(other);
            // damageable?.OnDamaged(rigidbody.velocity.magnitude, transform, other.contacts[0].point, other.contacts[0].normal);
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
    }
}
