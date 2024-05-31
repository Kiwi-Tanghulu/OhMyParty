using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileBlock : MonoBehaviour
    {
        [SerializeField] Collider[] blocks = null;

        public void SetActive(bool active)
        {
            blocks.ForEach(i => {
                i.isTrigger = !active;
                i.gameObject.SetActive(active);
            });
        }
    }
}
