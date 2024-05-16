using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class SetVerticalVelocityAction : PlayerFSMAction
    {
        [SerializeField] private float verticalVelocity;

        public bool OnEnter;
        public bool OnUpdate;
        public bool OnExit;

        private CharacterMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            if(OnEnter)
                movement.SetVerticalVelocity(verticalVelocity);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(OnUpdate)
                movement.SetVerticalVelocity(verticalVelocity);
        }

        public override void ExitState()
        {
            base.ExitState();

            if(OnExit)
                movement.SetVerticalVelocity(verticalVelocity);
        }
    }
}