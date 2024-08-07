using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static Controls;

namespace OMG.Inputs
{
    [CreateAssetMenu(menuName = "SO/PlayInputSO")]
    public class PlayInputSO : InputSO, IPlayActions
    {
        public Action<Vector2> OnMoveEvent;
        //public Action<Vector2> OnMouseDeltaEvent;
        public Action<bool> OnInteractEvent;
        public Action OnActionEvent;
        public Action OnActiveEvent;
        public Action<bool> OnJumpEvent;
        public Action OnEscapeEvent;
        public Action<int> OnScrollEvent;

        public bool MoveInputInversion = false;

        public Vector2 MouseDelta { get; private set; } = Vector2.zero;

        protected override void OnEnable()
        {
            base.OnEnable();

            PlayActions play = InputManager.controls.Play;
            play.SetCallbacks(this);
            InputManager.RegistInputMap(this, play.Get());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if(context.performed || context.canceled)
            {
                Vector2 input = context.ReadValue<Vector2>();
                if(MoveInputInversion)
                    input *= -1f;
                OnMoveEvent?.Invoke(input);
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
                OnInteractEvent?.Invoke(true);
            else if(context.canceled)
                OnInteractEvent?.Invoke(false);
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            if (context.started)
                OnActionEvent?.Invoke();

        }

        public void OnActive(InputAction.CallbackContext context)
        {
            if (context.started)
                OnActiveEvent?.Invoke();
        }

        public void OnMouseDelta(InputAction.CallbackContext context)
        {
            //if (context.started || context.performed)
            //    OnMouseDeltaEvent?.Invoke(context.ReadValue<Vector2>());
            //else if(context.canceled)
            //    OnMouseDeltaEvent?.Invoke(Vector2.zero);
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
                OnJumpEvent?.Invoke(true);
            if(context.canceled)
                OnJumpEvent?.Invoke(false);
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
            if (context.started)
                OnEscapeEvent?.Invoke();
        }

        public void OnScroll(InputAction.CallbackContext context)
        {
            if (!context.started)
                return;

            float value = context.ReadValue<Vector2>().y;
            if (value == 0)
                return;
            value = Mathf.Sign(value);

            OnScrollEvent?.Invoke((int)value);
        }
    }
}