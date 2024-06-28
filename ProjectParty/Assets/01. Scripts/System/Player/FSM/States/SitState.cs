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
        //[SerializeField] private float detectChairDistance = 1.5f;
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

        public override void EnterState()
        {
            base.EnterState();

            if (focuser.FocusedObject.CurrentObject.TryGetComponent<Chair>(out Chair chair))
            {
                usingChair = chair;
            }

            if (usingChair == null)
            {
                brain.ChangeState(brain.DefaultState);
            }
            else
            {
                sitPoint = usingChair.GetUseableSitPoint();
                usingChair.SetUseWhetherChair(sitPoint, true);
                movement.Teleport(sitPoint.position, sitPoint.rotation);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            
            usingChair?.SetUseWhetherChair(sitPoint, false);
        }
    }
}