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

            Debug.Log("1");

            if (other.TryGetComponent<PlayerController>(out PlayerController player) == false)
                return;

            Debug.Log("2");

            collisionEvent?.Invoke(player.OwnerClientId);
        }
    }
}
