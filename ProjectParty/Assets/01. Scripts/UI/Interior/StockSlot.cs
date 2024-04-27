using OMG.Interiors;
using UnityEngine;

namespace OMG.UI.Interiors
{
    public class StockSlot : MonoBehaviour
    {
        private InteriorPropSO propData = null;
        private InteriorSystem interiorSystem = null;
        private ShopPanel shopPanel = null;

        public void Init(InteriorPropSO data, InteriorSystem system, ShopPanel panel)
        {
            propData = data;
            interiorSystem = system;
            shopPanel = panel;
        }

        public void SetPropData()
        {
            interiorSystem.SetPropData(propData.PropID);
            shopPanel.Display(false);
        }
    }
}
