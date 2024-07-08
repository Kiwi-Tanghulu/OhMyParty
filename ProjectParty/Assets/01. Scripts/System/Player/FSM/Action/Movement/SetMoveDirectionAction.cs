using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.FSM;
using OMG.Inputs;

namespace OMG.Player.FSM
{
    public class SetMoveDirectionAction : PlayerFSMAction
    {
        [SerializeField] private PlayInputSO input;

        private CharacterMovement movement;

        private Vector3 inputMoveValue;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            movement = brain.GetComponent<CharacterMovement>();

            #region !use on network
#if UNITY_EDITOR
            if (!brain.Controller.UseInNetwork)
            {
                input.OnMoveEvent += SetInputValue;
                return;
            }
#endif
            #endregion

            if (brain.Controller.IsOwner)
                input.OnMoveEvent += SetInputValue;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            movement.SetMoveDirection(inputMoveValue);
        }

        private void OnDestroy()
        {
            #region !use on network
#if UNITY_EDITOR
            if (!brain.Controller.UseInNetwork)
            {
                input.OnMoveEvent -= SetInputValue;
                return;
            }
#endif
            #endregion

            if (brain.Controller.IsOwner)
                input.OnMoveEvent -= SetInputValue;
        }

        private void SetInputValue(Vector2 input)
        {
            inputMoveValue = new Vector3(input.x, 0f, input.y);
        }
    }
}
