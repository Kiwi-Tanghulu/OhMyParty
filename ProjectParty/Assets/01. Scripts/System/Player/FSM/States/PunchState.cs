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
            //Collider[] result = new Collider[1];
            //Physics.OverlapSphereNonAlloc(eyeTrm.position + actioningPlayer.transform.forward * distance, radius, result, layer);
            RaycastHit[] hits = Physics.SphereCastAll(eyeTrm.position + actioningPlayer.transform.forward * distance, radius, actioningPlayer.transform.forward, 0f, layer);

            //if (result[0] != null)
            //{
            //    if(result[0].TryGetComponent<IDamageable>(out IDamageable damageable))
            //    {
            //        damageable.OnDamaged(150f, actioningPlayer.transform);
            //    }
            //}

            if (hits.Length > 0)
            {
                for(int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.OnDamaged(150f, actioningPlayer.transform, hits[i].point);
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
            Gizmos.DrawWireSphere(eyeTrm.position + actioningPlayer.transform.forward * distance, radius);
        }
#endif
    }
}