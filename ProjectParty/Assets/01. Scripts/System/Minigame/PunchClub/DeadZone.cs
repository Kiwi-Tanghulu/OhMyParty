using OMG.Player;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.PunchClub
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] UnityEvent<ulong> onPlayerDeadEvent = null;
        private bool active = false;

        public void SetActive(bool active)
        {
            this.active = active;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger");
            if(active == false)
                return;

            Debug.Log(other.name);
            if(other.TryGetComponent<PlayerController>(out PlayerController player) == false)
                return;
            Debug.Log(player.OwnerClientId);

            onPlayerDeadEvent?.Invoke(player.OwnerClientId);
        }
    }
}
