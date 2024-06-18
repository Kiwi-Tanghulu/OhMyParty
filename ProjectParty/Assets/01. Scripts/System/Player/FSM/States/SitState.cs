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

        private CharacterMovement movement;
        private PlayerFocuser focuser;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();
            focuser = player.GetComponent<PlayerFocuser>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            if (focuser.FocusedObject.CurrentObject.TryGetComponent<Chair>(out Chair chair))
            {
                usingChair = chair;
                sitPoint = usingChair.GetUseableSitPoint();
            }

            if (usingChair == null)
            {
                brain.ChangeState(brain.DefaultState);
            }
            else
            {
                usingChair.SetUseWhetherChair(sitPoint, true);
            }
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();

            if(sitPoint != null)
                movement.Teleport(sitPoint.position, sitPoint.rotation);
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();
            
            usingChair?.SetUseWhetherChair(sitPoint, false);
        }
    }
}