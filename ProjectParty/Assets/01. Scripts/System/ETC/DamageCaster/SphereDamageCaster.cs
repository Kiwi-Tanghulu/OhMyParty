using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class SphereDamageCaster : DamageCaster
    {
        [SerializeField] private float distance = 1f;
        [SerializeField] private float radius = 0.5f;
        
#if UNITY_EDITOR
        [Space]
        [SerializeField] private bool DrawGizmo;
#endif

        public override RaycastHit[] DamageCast(Transform caster, float damage)
        {
            RaycastHit[] hits = Physics.SphereCastAll(eyeTrm.position - (eyeTrm.forward * radius * 2),
                radius, eyeTrm.transform.forward, (radius * 2) + distance);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform == caster.transform)
                        continue;
                    if (hits[i].point == Vector3.zero)
                        continue;

                    if (hits[i].collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.OnDamaged(damage, caster.transform, hits[i].point, hitEffectType, hits[i].normal);
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
            Gizmos.DrawLine(eyeTrm.position, eyeTrm.position + eyeTrm.forward * distance);
            Gizmos.DrawWireSphere(eyeTrm.position + eyeTrm.transform.forward * distance, radius);
        }
#endif
    }
}
