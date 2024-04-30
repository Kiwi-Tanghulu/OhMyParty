using System.Collections.Generic;
using OMG.Interiors;
using UnityEngine;

namespace OMG.UI.Interiors
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] Transform container = null;
        [SerializeField] InteriorSystem interiorSystem = null;
        [SerializeField] StockSlot slotPrefab = null;
        [SerializeField] List<PropListSO> propLists = new List<PropListSO>(new PropListSO[4]);

        public void Init(int index)
        {
            if(index < 0 || index > propLists.Count - 1)
                return;

            ClearSlots();

            PropListSO list = propLists[index];
            for(int i = 0; i < list.Count; ++i)
            {
                StockSlot slot = Instantiate(slotPrefab, container);
                slot.Init(list[i], interiorSystem, this);
            }
        }

        public void Display(bool active)
        {
            gameObject.SetActive(active);
        }

        private void ClearSlots()
        {
            foreach(Transform slot in container)
                Destroy(slot.gameObject);
        }
    }
}
