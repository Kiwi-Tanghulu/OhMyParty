using OMG.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Image;

namespace OMG.Player.FSM
{
    public class PunchState : ActionState
    {
        [SerializeField] private Transform eyeTrm;
        [SerializeField] private float distance = 1f;
        [SerializeField] private float radius = 0.5f;

        [Space]
        [SerializeField] private bool DrawGizmo;

        public override void DoAction()
        {
            base.DoAction();

            RaycastHit[] hits = Physics.SphereCastAll(eyeTrm.position - (eyeTrm.forward * radius * 2),
                radius, player.transform.forward, (radius * 2) + distance);
            
            if (hits.Length > 0)
            {
                for(int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform == player.transform)
                        continue;
                    if (hits[i].point == Vector3.zero)
                        continue;

                    if (hits[i].collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.OnDamaged(5f, player.transform, hits[i].point);
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmo)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(eyeTrm.position + player.transform.forward * distance, radius);
        }
#endif
    }
}