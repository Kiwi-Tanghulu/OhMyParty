using OMG.Interacting;
using UnityEngine;

namespace OMG.Lobbies
{
    public class MinigameSpotVisual : MonoBehaviour, IFocusable
    {
        private MinigameSpot minigameSpot = null;
        public GameObject CurrentObject => minigameSpot.gameObject;

        private void Awake()
        {
            minigameSpot = transform.parent.GetComponent<MinigameSpot>();
        }

        public void OnFocusBegin(Vector3 point)
        {
            // Set UI
        }

        public void OnFocusEnd()
        {
            
        }
    }
}
