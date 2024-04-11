using OMG.Input;
using OMG.Interacting;
using UnityEngine;

namespace OMG.Players
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] PlayInputSO input;

        private PlayerFocuser focuser = null;

        private IFocusable lastFocusedTarget = null;
        private IInteractable currentTarget = null;

        private void Awake()
        {
            input.OnInteractEvent += HandleInteract;
            focuser = GetComponent<PlayerFocuser>();
        }

        private void OnDestroy()
        {
            input.OnInteractEvent -= HandleInteract;
        }

        private void HandleInteract(bool actived)
        {
            if (focuser.IsEmpty)
                return;

            if (lastFocusedTarget != focuser.FocusedObject)
                currentTarget = focuser.FocusedObject.CurrentObject.GetComponent<IInteractable>();

            currentTarget?.Interact(this, actived, focuser.FocusedPoint);
        }
    }

}