using OMG.Player;
using UnityEngine;

namespace OMG.Minigames
{
    public class GearGimmick : Gimmick
    {
        [Space]
        [SerializeField] private Transform gearTrm;

        [Space]
        [SerializeField] private float effectPower;

        [Space]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float moveDir;
        [SerializeField] private float maxMoveDistance;

        [Space]
        [SerializeField] private float rotateSpeed;

        private IDamageable target;

        private void Update()
        {
            Move();
            Rotate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                target = collision.gameObject.GetComponent<IDamageable>();
                Execute();
            }
        }

        protected override void Execute()
        {
            base.Execute();

            target.OnDamaged(effectPower, transform,
                default, HitEffectType.Stun, default, Vector3.up);
        }

        protected override bool IsExecutable()
        {
            return true;
        }

        private void Move()
        {
            if(Mathf.Abs(gearTrm.localPosition.z) >= maxMoveDistance)
            {
                Vector3 pos = gearTrm.localPosition;
                pos.z = Mathf.Clamp(gearTrm.localPosition.z, -1f, 1f);
                gearTrm.localPosition = pos;
                moveDir *= -1;
            }

            gearTrm.position += moveDir * Vector3.right * moveSpeed * Time.deltaTime;
        }

        private void Rotate()
        {
            gearTrm.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        }
    }
}