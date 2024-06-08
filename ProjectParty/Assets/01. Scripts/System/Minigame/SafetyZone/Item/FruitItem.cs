using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FruitItem : SafetyZoneItem
    {
        [SerializeField] float power = 10f;

        public override void OnCollision(Collision other)
        {
            if(other.transform.TryGetComponent<IDamageable>(out IDamageable id))
                id?.OnDamaged(power, transform, other.contacts[0].point, other.contacts[0].normal);

            Destroy(gameObject);
        }
    }
}