using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Items
{
    public class BananaItem : Item
    {
        [SerializeField] private List<string> targetTags;
        private IDamageable target;

        private void OnTriggerEnter(Collider other)
        {
            if(targetTags.Contains(other.gameObject.tag))
            {
                if(other.TryGetComponent<IDamageable>(out target))
                    OnActive();
            }
        }

        public override void OnActive()
        {
            target.OnDamaged(0, transform, transform.position, HitEffectType.Stun);
            Destroy(gameObject);
        }
    }
}
