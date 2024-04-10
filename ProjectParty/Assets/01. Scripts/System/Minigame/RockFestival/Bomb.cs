using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class Bomb : Rock
    {
        [SerializeField] float explosionLatency = 3f;
        [SerializeField] float explosionRadius = 3f;
        [SerializeField] LayerMask collisionLayer = 0;
        
        [Space(15f)]
        [SerializeField] float explosionDamage = 30f;
        [SerializeField] Vector3 explosionOffset = new Vector3(0f, -0.3f, 0f);

        public override void Init()
        {
            base.Init();
            StartCoroutine(this.DelayCoroutine(explosionLatency, Explosion));
        }

        private void Explosion()
        {
            Collider[] others = Physics.OverlapSphere(transform.position, explosionRadius, collisionLayer);
            foreach(Collider other in others)
            {
                if(other.transform == transform)
                    continue;

                if(other.TryGetComponent<IDamageable>(out IDamageable id))
                {
                    Vector3 normal = transform.position - other.transform.position;
                    float factor = explosionRadius / (normal.magnitude + explosionRadius);

                    id.OnDamaged(explosionDamage * factor, transform, normal.normalized + explosionOffset);
                }
            }

            NetworkObject.Despawn(true);
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

        #endif
    }
}
