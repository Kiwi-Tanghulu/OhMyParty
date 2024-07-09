using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class SphereDamageCaster : DamageCaster
    {
        [SerializeField] private float distance = 1f;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float damage = 20f;
        
#if UNITY_EDITOR
        [Space]
        [SerializeField] private bool DrawGizmo;
#endif

        public override RaycastHit[] DamageCast(Transform attacker)
        {
            RaycastHit[] hits = Physics.SphereCastAll(eyeTrm.position - (eyeTrm.forward * radius * 2),
                radius, eyeTrm.transform.forward, (radius * 2) + distance);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform == attacker.transform)
                        continue;
                    if (hits[i].point == Vector3.zero)
                        continue;

                    if (hits[i].collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.OnDamaged(damage, attacker.transform, hits[i].point, hitEffectType);
                    }
                }
            }

            return hits;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmo)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(eyeTrm.position + eyeTrm.transform.forward * distance, radius);
        }
#endif
    }
}
