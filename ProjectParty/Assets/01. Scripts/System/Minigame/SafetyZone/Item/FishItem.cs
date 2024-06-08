using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class FishItem : SafetyZoneItem
    {
        [SerializeField] float power = 10f;

        public override void OnCollision(Collision other)
        {
            if(false) // 부딪힌 것이 본인 클라라면
                return;

            if(other.transform.TryGetComponent<IDamageable>(out IDamageable id))
                id?.OnDamaged(power, transform, other.contacts[0].point, other.contacts[0].normal);

            Destroy(gameObject);
        }
    }
}
