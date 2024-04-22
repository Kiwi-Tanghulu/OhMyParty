using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.Lobbies;
using OMG.Player;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class SitState : PlayerFSMState
    {
        [SerializeField] private float detectChairDistance = 1.5f;
        private Chair usingChair;
        private Transform sitPoint;

        private PlayerMovement movement;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {
            Collider[] cols = Physics.OverlapSphere(player.transform.position, detectChairDistance);
            for(int i = 0; i < cols.Length; i++)
            {
                if (cols[i].TryGetComponent<Chair>(out Chair chair))
                {
                    usingChair = chair;

                    sitPoint = usingChair.GetUseableSitPoint();

                    break;
                }
            }

            base.EnterState();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            if(usingChair == null)
            {
                brain.ChangeState(brain.DefaultState);
            }
            else
            {
                usingChair.SetUseWhetherChair(sitPoint, true);
                player.transform.position = sitPoint.position;
                player.transform.localRotation = sitPoint.rotation;
            }
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();
            
            usingChair?.SetUseWhetherChair(sitPoint, false);
        }
    }
}