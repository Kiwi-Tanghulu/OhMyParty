using Steamworks;
using System;
using System.Collections;
using Unity.Netcode.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    public class CharacterMovement : CharacterComponent
    {
        private MovementComponent movement;
        public MovementComponent Movement => movement;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            movement = GetComponent<MovementComponent>();
        }

        public override void PostInitializeComponent()
        {
            base.PostInitializeComponent();

            movement.Init(Controller.GetCharacterComponent<CharacterStat>().StatSO);
        }

        //override this method fucking jiseong
        public override void UpdateCompo()
        {
            base.UpdateCompo();

            //Move();
        }

        public virtual void Move()
        {
            movement.Move();
        }

        public virtual void SetMoveSpeed(float value)
        {
            movement.SetMoveSpeed(value);
        }

        public virtual void SetMoveDirection(Vector3 value, bool lookMoveDir = true, bool forceSet = false)
        {
            movement.SetMoveDirection(value, lookMoveDir, forceSet);
        }

        public void Teleport(Vector3 pos, Quaternion rot)
        {
            movement.Teleport(pos, rot);    
        }

        public void Jump()
        {
            movement.Jump();
        }

        public void Jump(float power)
        {
            movement.Jump(power);
        }

        public void Gravity()
        {
            movement.Gravity();
        }

        public void VerticalMove()
        {
            movement.VerticalMove();
        }

        #region ETC
        public void Knockback(Vector3 direction, float power, float time, Action onEndEvent)
        {
            SetMoveDirection(direction, false, true);

            StopAllCoroutines();

            StartCoroutine(KnockbackCo(power, time, onEndEvent));
        }

        private IEnumerator KnockbackCo(float power, float time, Action onEndEvent)
        {
            SetMoveSpeed(power);

            float currentTime = 0f;

            while(currentTime < time)
            {
                currentTime += Time.deltaTime;

                SetMoveSpeed(Mathf.Lerp(power, 0f, currentTime / time));

                yield return null;
            }
            SetMoveSpeed(0f);

            onEndEvent?.Invoke();

            SetMoveSpeed(Movement.StatSO[CharacterStatType.MaxMoveSpeed].Value);
        }
        #endregion
    }
}