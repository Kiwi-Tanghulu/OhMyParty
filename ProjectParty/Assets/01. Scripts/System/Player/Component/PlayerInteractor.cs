using OMG.Inputs;
using OMG.Interacting;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerInteractor : NetworkBehaviour
    {
        [SerializeField] PlayInputSO input;

        private PlayerFocuser focuser = null;

        private IFocusable lastFocusedTarget = null;
        private IInteractable currentTarget = null;

        private bool active;

        public override void OnNetworkSpawn()
        {
            if(IsOwner == false)
                return;

            input.OnInteractEvent += HandleInteract;
            focuser = GetComponent<PlayerFocuser>();

            SetActive(true);
        }

        public override void OnNetworkDespawn()
        {
            if(IsOwner == false)
                return;

            input.OnInteractEvent -= HandleInteract;
        }

        private void HandleInteract(bool actived)
        {
            if (actived == false)
                return;

            if (gameObject.activeInHierarchy == false)
                return;

            if (focuser.IsEmpty)
            {
                currentTarget = null;
                return;
            }

            if(active == false)
            {
                return;
            }

            if (lastFocusedTarget != focuser.FocusedObject)
                currentTarget = focuser.FocusedObject.CurrentObject.GetComponent<IInteractable>();
            Debug.Log("interact");
            currentTarget?.Interact(this, actived, focuser.FocusedPoint);
        }

        public void SetActive(bool value)
        {
            active = value;
        }
    }
}