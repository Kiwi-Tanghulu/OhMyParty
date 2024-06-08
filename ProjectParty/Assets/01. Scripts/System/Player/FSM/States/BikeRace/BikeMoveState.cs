using OMG.FSM;
using OMG.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class BikeMoveState : PlayerFSMState
    {
        [SerializeField] private PlayInputSO inputSO;

        [Space]
        [SerializeField] private float angularVelocityCoefficient;
        private float angularVelocity;
        private float slantDirection;
        private float slantValue;

        [Space]
        [SerializeField] private float handlingPower;
        private float handlingValue;
        
        private CharacterMovement movement;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();
            
            handlingValue = 0f;
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            Vector3 rotation = player.transform.eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;
            player.transform.rotation = Quaternion.Euler(rotation);

            slantDirection = 1f;
            movement.SetMoveSpeed(0f);
            slantValue = 0f;

            inputSO.OnMoveEvent += SetSlantDir;
            inputSO.OnMouseDeltaEvent += Handling;
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();

            //slantValue += movement.MaxMoveSpeed * angularVelocityCoefficient * slantDirection * Time.deltaTime;

            player.transform.rotation = Quaternion.Euler(new Vector3(0f, handlingValue, slantValue));
            movement.SetMoveDirection(brain.transform.forward, false);
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            inputSO.OnMoveEvent -= SetSlantDir;
            inputSO.OnMouseDeltaEvent -= Handling;
        }

        private void SetSlantDir(Vector2 input)
        {
            if (input.x == 0)
                return;

            slantDirection = -Mathf.Sign(input.x);
        }

        private void Handling(Vector2 delta)
        {
            float dir = Mathf.Sign(delta.x);

            if (delta.x == 0f)
                dir = 0f;

            handlingValue += handlingPower * dir * Time.deltaTime;
        }
    }
}