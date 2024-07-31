using OMG.Player;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class HammerGimmick : Gimmick
    {
        [SerializeField] private float effectPower;

        [Space]
        [SerializeField] private float maxAngle;
        [SerializeField] private float maxMoveSpeed;
        [SerializeField] private float minMoveSpeed;
        [SerializeField] private int startMoveDir = -1;
        private int moveDir;
        private float currentAngle;

        private List<PlayerController> contactTargets;
        private PlayerController target;

        private void Awake()
        {
            contactTargets = new List<PlayerController>();

            moveDir = startMoveDir;
        }

        private void Update()
        {
            Move();

            for (int i = 0; i < contactTargets.Count; i++)
            {
                target = contactTargets[i];

                if (IsExecutable())
                {
                    Execute();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                contactTargets.Add(collision.gameObject.GetComponent<PlayerController>());
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                contactTargets.Remove(collision.gameObject.GetComponent<PlayerController>());
            }
        }

        protected override void Execute()
        {
            if(target.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                base.Execute();

                contactTargets.Remove(target);

                damageable.OnDamaged(effectPower, transform,
                    default, HitEffectType.Stun, default, moveDir * Vector3.right);
            }
        }

        protected override bool IsExecutable()
        {
            CharacterMovement movement = target.GetCharacterComponent<CharacterMovement>();
            float targetMoveDir = movement.Movement.MoveDir.x;

            if (targetMoveDir == 0)
                return true;

            return Mathf.Sign(targetMoveDir) != moveDir;
        }

        private void Move()
        {
            if (Mathf.Abs(currentAngle) >= maxAngle)
                moveDir *= -1;
            float moveSpeed = Mathf.Lerp(maxMoveSpeed, minMoveSpeed, Mathf.Abs(currentAngle) / maxAngle);
            currentAngle += moveDir * moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
    }
}