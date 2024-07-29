using OMG.Items;
using UnityEngine;
using System.Collections.Generic;
using OMG.Inputs;
using UnityEngine.Events;

namespace OMG.Player
{
    public class PlayerItemHolder : CharacterComponent
    {
        [SerializeField] private PlayInputSO input;

        [Space]
        [SerializeField] private int maxItemCount;
        private int currentItemIndex;
        private List<Item> holdingItems;
        public Item CurrentItem => holdingItems[currentItemIndex];

        [Space]
        public UnityEvent OnGetItemEvent;
        public UnityEvent OnUseItemEvent;
        public UnityEvent OnChangeItemEvent;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            holdingItems = new List<Item>(new Item[maxItemCount]);
            currentItemIndex = 0;

            input.OnInteractEvent += (value) =>
            {
                if(value)
                    UseItem();
            };
            input.OnScrollEvent += (value) =>
            {
                ChangeItem(currentItemIndex + value);
            };
        }

        public void GetItem(Item item)
        {
            if (holdingItems.Count >= maxItemCount)
                return;

            holdingItems.Add(item);
            OnGetItemEvent?.Invoke();
        }

        public void UseItem()
        {
            if (CurrentItem == null)
                return;

            CurrentItem.Active();
            OnUseItemEvent?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            holdingItems.Remove(item);
        }

        public void ChangeItem(int index)
        {
            index = (index % maxItemCount + maxItemCount) % maxItemCount;
            Debug.Log(index);
            currentItemIndex = index;
            OnChangeItemEvent?.Invoke();
        }
    }
}
