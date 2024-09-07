using OMG.Player;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.CookingClass
{
    public class DispenserCollision : CharacterComponent
    {
        [SerializeField] UnityEvent<ulong> onPlayerEnterEvent = null;
        [SerializeField] UnityEvent<ulong> onPlayerExitEvent = null;

        private void OnTriggerEnter(Collider other)
            => TryPublishCollisionEvent(onPlayerEnterEvent, other);

        private void OnTriggerExit(Collider other)
            => TryPublishCollisionEvent(onPlayerExitEvent, other);

        private void TryPublishCollisionEvent(UnityEvent<ulong> collisionEvent, Collider other)
        {
            if(Controller.IsOwner == false)
                return;

            if (other.TryGetComponent<PlayerController>(out PlayerController player) == false)
                return;

            collisionEvent?.Invoke(player.OwnerClientId);
        }
    }
}
