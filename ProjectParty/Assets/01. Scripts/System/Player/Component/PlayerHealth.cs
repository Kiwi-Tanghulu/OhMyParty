using OMG.FSM;
using OMG.Player.FSM;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Player
{
    public class PlayerHealth : NetworkBehaviour, IDamageable
    {
        private Transform attacker;
        private float damage;
        private Vector3 hitDir;
        private Vector3 hitPoint;

        public UnityEvent<Vector3 /*hit point*/> OnDamagedEvent;

        private FSMBrain fsm;

        public Transform Attacker => attacker;
        public float Damage => damage;
        public Vector3 HitDir => hitDir;
        public Vector3 HitPoint => hitPoint;

        private void Awake()
        {
            fsm = GetComponent<FSMBrain>();
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
        {
            this.attacker = attacker;
            hitDir = (transform.position - attacker.position).normalized;
            OnDamagedClientRpc(damage, point, hitDir);

            //this.damage = damage;
            //hitPoint = point;

            //OnDamagedEvent?.Invoke(point);

            //if (IsServer)
            //    fsm.ChangeState(typeof(StunState));
        }

        [ClientRpc]
        public void OnDamagedClientRpc(float damage, Vector3 point, Vector3 hitDir)
        {
            this.damage = damage;
            this.hitDir = hitDir;
            hitPoint = point;

            OnDamagedEvent?.Invoke(point);

            if (IsServer)
            {
                if (damage == -1)
                    fsm.ChangeState(typeof(DieState));
                else
                    fsm.ChangeState(typeof(StunState));
            }
        }
    }
}