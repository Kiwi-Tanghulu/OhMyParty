using OMG.Extensions;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class LogGimmick : Gimmick
    {
        [SerializeField] private float destroyDelayTime;

        [Space]
        [SerializeField] private float effectPower;

        [Space]
        [SerializeField] private float moveSpeed;

        private IDamageable target;
        private Collision targetCollision;

        private void Start()
        {
            this.DelayCoroutine(destroyDelayTime, () => Destroy(gameObject));
        }

        private void Update()
        {
            Move();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<IDamageable>(out target))
                {
                    targetCollision = collision;
                    Execute();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LogBlocker"))
            {
                Destroy(gameObject);
            }
        }

        protected override void Execute()
        {
            base.Execute();

            target.OnDamaged(effectPower, transform,
                targetCollision.GetContact(0).point, HitEffectType.Stun, default, Vector3.left);

            Destroy(gameObject);
        }

        protected override bool IsExecutable()
        {
            return true;
        }

        private void Move()
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }
}
