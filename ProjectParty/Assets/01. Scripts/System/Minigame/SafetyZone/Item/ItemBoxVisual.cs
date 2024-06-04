using OMG.Interacting;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class ItemBoxVisual : MonoBehaviour, IFocusable
    {
        private ItemBox itemBox = null;
        public GameObject CurrentObject => itemBox.gameObject;

        private void Awake()
        {
            itemBox = transform.parent.GetComponent<ItemBox>();
        }

        public void OnFocusBegin(Vector3 point)
        {
        }

        public void OnFocusEnd()
        {
        }
    }
}
