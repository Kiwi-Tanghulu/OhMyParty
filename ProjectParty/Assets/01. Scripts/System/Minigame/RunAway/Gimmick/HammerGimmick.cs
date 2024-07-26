using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class HammerGimmick : Gimmick
    {
        public struct ContactPlayer
        {
            public PlayerController Player;
            public Vector3 Point;
        }

        [SerializeField] private float effectPower;
        [SerializeField] private float moveAngle;
        [SerializeField] private float maxMoveSpeed;
        private int moveDir;

        private List<PlayerController> contactTargets;
        private PlayerController target;

        private void Awake()
        {
            contactTargets = new List<PlayerController>();
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

        public override void Execute()
        {
            if(target.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
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

        }
    }
}