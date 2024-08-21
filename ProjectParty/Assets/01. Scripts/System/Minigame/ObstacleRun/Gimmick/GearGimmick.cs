using OMG.Player;
using UnityEngine;

namespace OMG.Minigames
{
    public class GearGimmick : Gimmick
    {
        //[Space]
        //[SerializeField] private Transform gearTrm;

        [Space]
        [SerializeField] private float effectPower;

        [Space]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float moveDir;
        [SerializeField] private float maxMoveDistance;

        [Space]
        [SerializeField] private float rotateSpeed;

        private IDamageable target;
        private Collision targetCollision;

        private void Update()
        {
            Move();
            Rotate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(collision.gameObject.TryGetComponent<IDamageable>(out target))
                {
                    targetCollision = collision;
                    Execute();
                }
            }
        }

        protected override void Execute()
        {
            base.Execute();

            target.OnDamaged(effectPower, transform,
                targetCollision.GetContact(0).point, HitEffectType.Stun, default, Vector3.up);
        }

        protected override bool IsExecutable()
        {
            return true;
        }

        private void Move()
        {
            if(Mathf.Abs(transform.localPosition.z) >= maxMoveDistance)
            {
                Vector3 pos = transform.localPosition;
                pos.z = Mathf.Clamp(transform.localPosition.z, -1f, 1f);
                transform.localPosition = pos;
                moveDir *= -1;
            }

            transform.position += moveDir * transform.parent.forward * moveSpeed * Time.deltaTime;
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        }
    }
}