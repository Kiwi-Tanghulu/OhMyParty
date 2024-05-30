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
        private Vector3 hitPoint;

        [SerializeField] private Transform damagedTrm;

        public UnityEvent OnDamagedEvent;

        public Transform Attacker => attacker;
        public float Damage => damage;
        public Vector3 HitDir => hitDir;
        public Vector3 HitPoint => hitPoint;

        public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
        {
            this.attacker = attacker;
            this.damage = damage;
            hitDir = (transform.position - attacker.position).normalized;
            hitPoint = point;

            OnDamagedEvent?.Invoke();
        }

        public Transform GetDamagedTransfrom()
        {
            return damagedTrm;
        }
    }
}