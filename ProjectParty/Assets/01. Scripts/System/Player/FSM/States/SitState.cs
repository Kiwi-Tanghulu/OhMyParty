using OMG.Lobbies;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Players
{
    public class SitState : FSMState
    {
        [SerializeField] private float detectChairDistance = 1.5f;
        private Chair usingChair;
        private Transform sitPoint;

        public override void EnterState()
        {
            Collider[] cols = Physics.OverlapSphere(actioningPlayer.transform.position, detectChairDistance);
            for(int i = 0; i < cols.Length; i++)
            {
                if (cols[i].TryGetComponent<Chair>(out Chair chair))
                {
                    usingChair = chair;

                    sitPoint = usingChair.GetUseableSitPoint();

                    break;
                }
            }

            movement.ApplyGravity = false;

            base.EnterState();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            if(usingChair == null)
            {
                actioningPlayer.ChangeState(actioningPlayer.DefaultState);
            }
            else
            {
                usingChair.SetUseWhetherChair(sitPoint, true);
                actioningPlayer.transform.position = sitPoint.position;
                actioningPlayer.transform.localRotation = sitPoint.rotation;
            }
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();
            
            usingChair?.SetUseWhetherChair(sitPoint, false);
        }
    }
}