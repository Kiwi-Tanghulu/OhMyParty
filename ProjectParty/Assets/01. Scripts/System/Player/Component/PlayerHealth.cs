using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        private Transform attacker;
        private float damage;
        private Vector3 hitDir;

        public UnityEvent OnHitEvent;

        public Transform Attacker => attacker;
        public float Damage => damage;
        public Vector3 HitDir => hitDir;

        public void OnDamaged(float damage, Transform attacker)
        {
            this.attacker = attacker;
            this.damage = damage;
            hitDir = (transform.position - attacker.position).normalized;

            OnHitEvent?.Invoke();
        }
    }
}