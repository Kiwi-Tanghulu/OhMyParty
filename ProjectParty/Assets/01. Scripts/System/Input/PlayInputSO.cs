using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Input
{
    [CreateAssetMenu(menuName = "SO/PlayInputSO")]
    public class PlayInputSO : InputSO, IPlayActions
    {
        public Action<Vector2> OnMoveAction;

        protected override void OnEnable()
        {
            base.OnEnable();

            PlayActions play = InputManager.controls.Play;
            play.SetCallbacks(this);
            InputManager.RegistInputMap(this, play.Get());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveAction?.Invoke(context.ReadValue<Vector2>());
            Debug.Log(context.ReadValue<Vector2>());
        }
    }
}