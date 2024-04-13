using OMG.Input;
using OMG.Interacting;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Players
{
    public class PlayerInteractor : NetworkBehaviour
    {
        [SerializeField] PlayInputSO input;

        private PlayerFocuser focuser = null;

        private IFocusable lastFocusedTarget = null;
        private IInteractable currentTarget = null;

        public override void OnNetworkSpawn()
        {
            if(IsOwner == false)
                return;

            input.OnInteractEvent += HandleInteract;
            focuser = GetComponent<PlayerFocuser>();
        }

        public override void OnNetworkDespawn()
        {
            if(IsOwner == false)
                return;

            input.OnInteractEvent -= HandleInteract;
        }

        private void HandleInteract(bool actived)
        {
            if(gameObject.activeSelf == false)
                return;

            if (focuser.IsEmpty)
            {
                currentTarget = null;
                return;
            }

            if (lastFocusedTarget != focuser.FocusedObject)
                currentTarget = focuser.FocusedObject.CurrentObject.GetComponent<IInteractable>();
            Debug.Log(currentTarget);
            currentTarget?.Interact(this, actived, focuser.FocusedPoint);
        }
    }

}