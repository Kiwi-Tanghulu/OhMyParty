using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Input
{
    [CreateAssetMenu(menuName = "SO/Input/UIInputSO")]
    public class UIInputSO : InputSO, IUIActions
    {
        public event Action OnSpaceEvent = null;
        public event Action OnInteractEvent = null;

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
    }
}
