using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OMG.Input;

namespace OMG.Player
{
    public class TestActioningPlayer : ActioningPlayer
    {
        [SerializeField] private PlayInputSO input;

        private PlayerMovement movement;

        public override void InitActioningPlayer()
        {
            movement = GetComponent<PlayerMovement>();

            input.OnMoveAction += SetMoveDie;
        }

        public override void UpdateActioningPlayer()
        {
            movement.Move();
        }

        private void SetMoveDie(Vector2 input)
        {
            Vector3 moveDir = new Vector3(input.x, 0f, input.y);

            movement.SetMoveDir(moveDir);   
        }
    }
}