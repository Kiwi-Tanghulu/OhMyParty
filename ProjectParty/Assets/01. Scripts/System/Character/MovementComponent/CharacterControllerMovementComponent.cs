using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class CharacterControllerMovementComponent : MovementComponent
    {
        protected UnityEngine.CharacterController characterController;
        public UnityEvent<ControllerColliderHit> OnColliderHit;
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

        public override void VerticalMove()
        {
            base.VerticalMove();

            if (characterController.enabled)
                characterController.Move(Vector3.up * VerticalVelocity * Time.deltaTime);
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

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            OnColliderHit?.Invoke(hit);
        }
    }
}