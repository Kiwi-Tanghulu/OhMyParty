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

        private List<Collision> contactCollisions;

        private void Awake()
        {
            contactCollisions = new List<Collision>();
        }

        private void Update()
        {
            Move();

            for (int i = 0; i < contactCollisions.Count; i++)
            {
                //if (IsExecutable(contactCollisions[i]))
                //{
                //    Execute(contactCollisions[i]);
                //}
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerController>
                (out PlayerController player))
            {
                contactCollisions.Add(collision);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerController>
                (out PlayerController player))
            {
                contactCollisions.Remove(collision);
            }
        }

        public override void Execute(Transform target)
        {
            if(target.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                //damageable.OnDamaged(effectPower, transform,
                //    target.GetContact(0).point, HitEffectType.Stun);
            }
        }

        protected override bool IsExecutable(Transform target)
        {
            if(target.gameObject.TryGetComponent<CharacterMovement>(out CharacterMovement movement))
            {
                float targetMoveDir = movement.Movement.MoveDir.x;

                if (targetMoveDir == 0)
                    return true;

                return Mathf.Sign(targetMoveDir) != moveDir;
            }
            else
            {
                return false;
            }
        }

        private void Move()
        {

        }
    }
}