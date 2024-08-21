using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Inputs
{
    [CreateAssetMenu(menuName = "SO/Input/UIInputSO")]
    public class UIInputSO : InputSO, IUIActions
    {
        public event Action OnSpaceEvent = null;
        public event Action OnInteractEvent = null;
        public event Action OnBackEvent = null;
        public event Action<bool> OnLeftClickEvent = null;
        public event Action OnChatEvent;

        public Vector3 MousePosition { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();

            UIActions ui = InputManager.controls.UI;
            ui.SetCallbacks(this);
            InputManager.RegistInputMap(this, ui.Get());
        }

        public void OnSpace(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnSpaceEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnInteractEvent?.Invoke();
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnLeftClickEvent?.Invoke(true);
            else if(context.canceled)
                OnLeftClickEvent?.Invoke(false);
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            if(context.started)
            OnBackEvent?.Invoke();
        }

        public void OnChat(InputAction.CallbackContext context)
        {
            if (context.started)
                OnChatEvent?.Invoke();
        }
    }
}
