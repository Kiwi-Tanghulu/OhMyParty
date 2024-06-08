using OMG.Interacting;
using OMG.Timers;
using OMG.UI.Minigames.SafetyZones;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class ItemBoxVisual : MonoBehaviour, IFocusable
    {
        private static ItemBox focusedItemBox = null;

        [SerializeField] Sprite itemIcon = null;
        private ItemBox itemBox = null;
        public GameObject CurrentObject => itemBox.gameObject;

        private ItemBoxPanel itemBoxPanel = null;
        private Timer timer = null;
        
        private void Awake()
        {
            itemBox = transform.parent.GetComponent<ItemBox>();
            itemBoxPanel = DEFINE.MinigameCanvas.Find("ItemBoxPanel").GetComponent<ItemBoxPanel>();
            timer = itemBox.GetComponent<Timer>();
        }

        public void OnFocusBegin(Vector3 point)
        {
            focusedItemBox = itemBox;
            itemBoxPanel.Init(point, timer, itemIcon);
        }

        public void OnFocusEnd()
        {
            if(focusedItemBox == itemBox)
                itemBoxPanel.Release();
        }
    }
}
