using OMG.Extensions;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class LogGimmick : Gimmick
    {
        [SerializeField] private float destroyDelayTime;

        [Space]
        [SerializeField] private float effectPower;

        [Space]
        [SerializeField] private float moveSpeed;

        [Space]
        public UnityEvent OnGroundEvent;
        public UnityEvent<Vector3> OnBreakEvent;

        private IDamageable target;
        private Collision targetCollision;

        private Vector3 moveDir;

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
            if (collision.gameObject.CompareTag("OtherPlayer"))
            {
                OnBreakEvent?.Invoke(transform.position);

                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Ground"))
            {
                OnGroundEvent?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LogBlocker"))
            {
                OnBreakEvent?.Invoke(transform.position);

                Destroy(gameObject);
            }
        }

        public void SetMoveDirection(Vector3 dir)
        {
            moveDir = dir;
        }

        protected override void Execute()
        {
            base.Execute();

            target.OnDamaged(effectPower, transform,
                targetCollision.GetContact(0).point, HitEffectType.Stun, default, Vector3.left);
            OnBreakEvent?.Invoke(transform.position);

            Destroy(gameObject);
        }

        protected override bool IsExecutable()
        {
            return true;
        }

        private void Move()
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }
}
