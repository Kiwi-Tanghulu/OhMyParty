using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class StopAction : PlayerFSMAction
    {
        private CharacterMovement movement;

        [SerializeField] private bool onEnter;
        [SerializeField] private bool onExit;

        [DrawIf("onEnter", true)]
        [SerializeField] private bool save;
        private Vector3 saveMoveDir;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = player.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            if (onEnter)
            {
                if (save)
                    saveMoveDir = movement.Movement.MoveDir;

                movement.SetMoveDirection(Vector3.zero);
            }
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (onEnter)
            {
                if (save)
                    saveMoveDir = movement.Movement.MoveDir;

                movement.SetMoveDirection(Vector3.zero);
//                Debug.Log(movement.Movement.MoveDir);
            }
        }

        public override void ExitState()
        {
            base.ExitState();

            if (onExit)
                movement.SetMoveDirection(Vector3.zero);

            if (save)
                movement.SetMoveDirection(saveMoveDir);
        }
    }
}