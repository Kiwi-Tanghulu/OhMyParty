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
        private List<PlayerItem> holdingItems;
        public PlayerItem CurrentItem => holdingItems[currentItemIndex];

        [Space]
        public UnityEvent OnGetItemEvent;
        public UnityEvent OnUseItemEvent;
        public UnityEvent OnChangeItemEvent;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            holdingItems = new List<PlayerItem>(new PlayerItem[maxItemCount]);
            currentItemIndex = 0;


            input.OnScrollEvent += OnScrollEvent;
        }

        private void OnDestroy()
        {
            input.OnScrollEvent -= OnScrollEvent;
        }

        public void GetItem(PlayerItem item)
        {
            if (holdingItems.Count >= maxItemCount)
                return;

            item.transform.position = transform.position;
            item.transform.SetParent(transform);
            item.SetOwnerPlayer(Controller as PlayerController);
            item.Init();
            holdingItems.Add(item);
            OnGetItemEvent?.Invoke();
        }

        public void UseItem()
        {
            if (CurrentItem == null)
                return;

            CurrentItem.Active();
            OnUseItemEvent?.Invoke();
            RemoveItem(CurrentItem);
        }

        public void RemoveItem(PlayerItem item)
        {
            holdingItems.Remove(item);
        }

        public void ChangeItem(int index)
        {
            index = (index % maxItemCount + maxItemCount) % maxItemCount;
            currentItemIndex = index;
            OnChangeItemEvent?.Invoke();
        }

        private void OnScrollEvent(int value)
        {
            ChangeItem(currentItemIndex + value);
        }
    }
}
