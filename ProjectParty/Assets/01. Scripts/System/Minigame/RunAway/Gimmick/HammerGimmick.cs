using OMG.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent OnSwingEvent;
        private bool isSwinged;

        private PlayerController target;
        private Collision targetCollision;

        private void Awake()
        {
            moveDir = startMoveDir;
        }

        private void Update()
        {
            Move();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                targetCollision = collision;
                target = targetCollision.gameObject.GetComponent<PlayerController>();
                if (IsExecutable())
                {
                    Execute();
                }
            }
        }

        protected override void Execute()
        {
            base.Execute();

            target.GetComponent<IDamageable>().OnDamaged(effectPower, transform,
                targetCollision.GetContact(0).point, HitEffectType.Stun, default, moveDir * Vector3.right);
        }

        protected override bool IsExecutable()
        {
            if (target == null)
                return false;

            CharacterMovement movement = target.GetCharacterComponent<CharacterMovement>();
            float targetMoveDir = movement.Movement.MoveDir.x;

            return (Mathf.Sign(targetMoveDir) != moveDir || targetMoveDir == 0) && 
                target.GetComponent<IDamageable>() != null;
        }

        private void Move()
        {
            if (Mathf.Abs(currentAngle) >= maxAngle)
            {
                moveDir *= -1;
                isSwinged = false;
            }

            float moveSpeed = Mathf.Lerp(maxMoveSpeed, minMoveSpeed, Mathf.Abs(currentAngle) / maxAngle);
            currentAngle += moveDir * moveSpeed * Time.deltaTime;
            if (Mathf.Abs(currentAngle) < 15f && isSwinged == false)
            {
                isSwinged = true;
                OnSwingEvent?.Invoke();
            }

            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
    }
}