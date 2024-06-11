using OMG.Interacting;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Lobbies
{
    public class MinigameSpotVisual : MonoBehaviour, IFocusable
    {
        private MinigameSpot minigameSpot = null;
        public GameObject CurrentObject => minigameSpot.gameObject;

        public UnityEvent<bool> OnFocusedEvent;

        private void Awake()
        {
            minigameSpot = transform.parent.GetComponent<MinigameSpot>();
        }

        public void OnFocusBegin(Vector3 point)
        {
            // Set UI
            OnFocusedEvent?.Invoke(true);
        }

        public void OnFocusEnd()
        {
            OnFocusedEvent?.Invoke(false);
        }
    }
}
