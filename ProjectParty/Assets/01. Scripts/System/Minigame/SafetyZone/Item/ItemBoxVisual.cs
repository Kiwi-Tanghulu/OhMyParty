using OMG.Interacting;
using OMG.Timers;
using OMG.UI.Minigames.SafetyZones;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class ItemBoxVisual : MonoBehaviour, IFocusable
    {
        private ItemBox itemBox = null;
        public GameObject CurrentObject => itemBox.gameObject;

        private ItemBoxPanel itemBoxPanel = null;
        private Timer timer = null;
        
        private void Awake()
        {
            itemBox = transform.parent.GetComponent<ItemBox>();
            itemBoxPanel = itemBox.ItemBoxPanel;
            timer = itemBox.GetComponent<Timer>();
        }

        public void OnFocusBegin(Vector3 point)
        {
            if(timer.Finished == false)
                return;

            itemBoxPanel.Display(true);
        }

        public void OnFocusEnd()
        {
            if(timer.Finished == false)
                return;

            itemBoxPanel.Display(false);
        }
    }
}
