using OMG.Player.FSM;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private LayerMask layer;

        [Space]
        [SerializeField] private bool DrawGizmo;

        protected override void DoAction()
        {
            base.DoAction();

            if (!player.IsServer)
                return;

            if (Physics.SphereCast(eyeTrm.position - (eyeTrm.forward * radius * 2), radius, player.transform.forward, out RaycastHit hit, (radius * 2) + distance))
            {
                //if (hit.transform == player.transform)
                //    return;
                Debug.Log(2);

                if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    Debug.Log(hit.transform.name);
                    damageable.OnDamaged(5f, player.transform, hit.point);
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