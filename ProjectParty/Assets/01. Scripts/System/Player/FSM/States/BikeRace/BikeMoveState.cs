using OMG.FSM;
using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM.Bike
{
    public class BikeMoveState : PlayerFSMState
    {
        [SerializeField] private PlayInputSO inputSO;

        [Space]
        [SerializeField] private float angularVelocityCoefficient;
        private float angularVelocity;
        private float slantDirection;

        [Space]
        [SerializeField] private float handlingPower;
        private float handlingValue;
        private Vector3 onEnterForawdDir;
        
        private PlayerMovement movement;

        private BikeRacePlayerController bikeRacePlayer;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            bikeRacePlayer = player as BikeRacePlayerController;
            movement = player.GetComponent<PlayerMovement>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            inputSO.OnMoveEvent += SetSlantDir;
            inputSO.OnMouseDeltaEvent += Handling;
            bikeRacePlayer.OnContectGround += OnContectGround;

            slantDirection = 1f;
            handlingValue = 0f;
            onEnterForawdDir = player.transform.forward;
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();

            Vector3 moveDir = Quaternion.Euler(0f, handlingValue, 0f) * onEnterForawdDir;
            movement.SetMoveDir(moveDir, false);

            angularVelocity = movement.MoveSpeed * angularVelocityCoefficient;
            player.transform.Rotate(Vector3.forward, angularVelocity * slantDirection * Time.deltaTime);
            Vector3 angle = player.transform.rotation.eulerAngles;
            angle.y = handlingValue;
            player.transform.rotation = Quaternion.Euler(angle);
        }

        protected override void OwnerExitState()
        {
            base.OwnerExitState();

            inputSO.OnMoveEvent -= SetSlantDir;
            inputSO.OnMouseDeltaEvent -= Handling;
            bikeRacePlayer.OnContectGround -= OnContectGround;
        }

        private void SetSlantDir(Vector2 input)
        {
            if (input.x == 0)
                return;

            slantDirection = -Mathf.Sign(input.x);
        }

        private void Handling(Vector2 delta)
        {
            if (delta.x == 0f)
                return;

            float dir = Mathf.Sign(delta.x);

            handlingValue += handlingPower * dir * Time.deltaTime;
        }

        private void OnContectGround()
        {
            Vector3 rotation = player.transform.eulerAngles;
            rotation.z = 0f;

            player.transform.rotation = Quaternion.Euler(rotation);
            movement.MoveSpeed = 0f;
        }
    }
}