using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class StopAction : PlayerFSMAction
    {
        public bool OnEnter;
        public bool OnUpdate;
        public bool OnExit;

        private PlayerMovement movement;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            movement = player.GetComponent<PlayerMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            if(OnEnter)
            {
                movement.SetMoveDir(Vector3.zero);
            }
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (OnUpdate)
                movement.SetMoveDir(Vector3.zero);
        }

        public override void ExitState()
        {
            base.ExitState();

            if (OnExit)
                movement.SetMoveDir(Vector3.zero);
        }
    }
}