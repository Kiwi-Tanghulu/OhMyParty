using UnityEngine;
using OMG.FSM;

namespace OMG.Player.FSM
{
    public class SitState : PlayerFSMState
    {
        private Chair usingChair;
        private Transform sitPoint;

        private CharacterMovement movement;
        private PlayerHealth health;
        private PlayerFocuser focuser;

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();
            focuser = player.GetComponent<PlayerFocuser>();
            health = player.GetComponent<PlayerHealth>();
        }

        public override void EnterState()
        {
            base.EnterState();

            health.Hitable = false;
            health.PlayerHitable = false;

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

            health.Hitable = true;
            health.PlayerHitable = true;

            usingChair?.SetUseWhetherChair(sitPoint, false);
        }
    }
}