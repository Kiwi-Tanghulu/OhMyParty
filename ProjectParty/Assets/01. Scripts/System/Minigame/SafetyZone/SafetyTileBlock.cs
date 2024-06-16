using OMG.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileBlock : MonoBehaviour
    {
        [SerializeField] Collider[] blocks = null;

        public UnityEvent<bool> OnActiveChangedEvent;

        public void SetActive(bool active)
        {
            //blocks.ForEach(i => {
            //    i.isTrigger = !active;
            //    i.gameObject.SetActive(active);
            //});

            OnActiveChangedEvent?.Invoke(active);
        }
    }
}
