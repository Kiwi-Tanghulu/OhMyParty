using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
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
            Collider[] result = new Collider[1];
            Physics.OverlapSphereNonAlloc(eyeTrm.position + actioningPlayer.transform.forward * distance, radius, result, layer);

            if (result[0] != null)
            {
                if(result[0].TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.OnDamaged(0f, actioningPlayer.transform);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmo)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(eyeTrm.position + actioningPlayer.transform.forward * distance, radius);
        }
#endif
    }
}