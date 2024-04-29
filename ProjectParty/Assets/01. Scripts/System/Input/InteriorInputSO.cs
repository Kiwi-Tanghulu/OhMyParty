using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Input
{
    [CreateAssetMenu(menuName = "SO/Input/InteriorInputSO")]
    public class InteriorInputSO : InputSO, IInteriorActions
    {
        public Action OnPlaceEvent = null;
        public Action OnCancelEvent = null;
        public Action<int> OnRotateEvent = null;

        public Vector2 PlacePosition { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();

            InteriorActions interior = InputManager.controls.Interior;
            interior.SetCallbacks(this);
            InputManager.RegistInputMap(this, interior.Get());
        }

        public void OnPlacePosition(InputAction.CallbackContext context)
        {
            PlacePosition = context.ReadValue<Vector2>();
        }

        public void OnPlace(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnPlaceEvent?.Invoke();
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnRotateEvent?.Invoke(Mathf.RoundToInt(context.ReadValue<float>()));
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnCancelEvent?.Invoke();
        }
    }
}