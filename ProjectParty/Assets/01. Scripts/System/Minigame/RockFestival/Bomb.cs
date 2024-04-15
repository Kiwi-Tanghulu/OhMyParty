using OMG.Extensions;
using OMG.Tweens;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.RockFestival
{
    public class Bomb : Rock
    {
        [SerializeField] UnityEvent onExplosionEvent = null;

        [SerializeField] float explosionLatency = 3f;
        [SerializeField] float explosionRadius = 3f;
        [SerializeField] LayerMask collisionLayer = 0;
        
        [Space(15f)]
        [SerializeField] float explosionDamage = 30f;
        [SerializeField] Vector3 explosionOffset = new Vector3(0f, -0.3f, 0f);

        [Space(15f)]
        [SerializeField] TweenSO bombScalingTween = null;

        protected override void Awake()
        {
            base.Awake();
            bombScalingTween = bombScalingTween.CreateInstance(transform);
        }

        private void Start()
        {
            BombScalingLoop();
        }

        public override void Init()
        {
            base.Init();
            StartCoroutine(this.DelayCoroutine(explosionLatency, Explosion));
        }

        private void Explosion()
        {
            bombScalingTween.ForceKillTween();

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

            CurrentHolder?.Release();
            NetworkObject.Despawn(true);
            onExplosionEvent?.Invoke();
        }

        private void BombScalingLoop()
        {
            bombScalingTween.PlayTween(BombScalingLoop);
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
