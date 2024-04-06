using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Input
{
    [CreateAssetMenu(menuName = "SO/PlayInputSO")]
    public class PlayInputSO : InputSO, IPlayActions
    {
        public Action<Vector2> OnMoveEvent;
        public Action<bool> OnInteractEvent;
        public Action OnActionEvent;

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
                OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
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
    }
}