using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class CharacterControllerMovementComponent : MovementComponent
    {
        protected UnityEngine.CharacterController characterController;

        public override void Init(CharacterStatSO statSO)
        {
            base.Init(statSO);

            characterController = GetComponent<UnityEngine.CharacterController>();
        }

        public override void Move()
        {
            base.Move();
            
            if(characterController.enabled)
                characterController.Move(moveVector);
        }

        protected override void CalcMoveVector()
        {
            base.CalcMoveVector();

            moveVector *= Time.deltaTime;
        }

        public override void SetCollisionActive(bool active)
        {
            characterController.detectCollisions = active;
        }

        public override void Teleport(Vector3 pos, Quaternion rot)
        {
            characterController.enabled = false;
            networkTrm.Teleport(pos, rot, transform.localScale);
            characterController.enabled = true;
        }
    }
}