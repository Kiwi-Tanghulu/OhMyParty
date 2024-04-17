using OMG.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class PunchState : ActionState
    {
        [SerializeField] private Transform eyeTrm;
        [SerializeField] private float distance = 1f;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private LayerMask layer;

        [Space]
        [SerializeField] private bool DrawGizmo;

        protected override void DoAction()
        {
            RaycastHit[] hits = Physics.SphereCastAll(eyeTrm.position + player.transform.forward * distance,
                radius, player.transform.forward, 0f, layer);

            if (hits.Length > 0)
            {
                for(int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        Debug.Log($"hit : {hits[i].transform.name}");
                        damageable.OnDamaged(150f, player.transform, hits[i].point);
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