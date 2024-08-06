using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class SlashGimmick : Gimmick
    {
        [SerializeField] private float turnSpeed;
        [SerializeField] private float hitPower;

        private void Update()
        {
            transform.Rotate(turnSpeed * Vector3.up);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.OnDamaged(hitPower, transform,
                        collision.GetContact(0).point,
                        HitEffectType.Stun, default,
                        (collision.GetContact(0).point - collision.transform.position).normalized);
                }
            }
        }

        protected override bool IsExecutable()
        {
            return true;
        }
    }
}