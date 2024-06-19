using OMG.Inputs;
using OMG.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace OMG.Minigames.MazeAdventure
{
    public enum ItemType
    {
        None,
        MakeWall,
        Invisibility,
    }
public class ItemSystem : MonoBehaviour, IPlayerCollision
    {
        [SerializeField] private PlayInputSO input;
        [SerializeField] private UnityEvent<Vector3> OnhitItemBox;
        private Dictionary<ItemType, MazeAdventureItem> playerItemDictionary = null;
        private ItemType currentItemType;
        private NetworkObject networkObject = null;

        public event Action<ItemType> OnItemChange;
        public event Action OnUseItem;
        public UnityEvent OnGetItme;

        public void Init(Transform playerTrm)
        {    
            networkObject = playerTrm.GetComponent<NetworkObject>();

            playerItemDictionary = new Dictionary<ItemType, MazeAdventureItem>();
            foreach(Transform itemTrm in transform)
            {
                if(itemTrm.TryGetComponent<MazeAdventureItem>(out MazeAdventureItem item))
                {
                    item.Init(playerTrm);
                    playerItemDictionary.Add(item.ItemType, item);
                }
            }

            if (networkObject.IsOwner == false)
                return;

            input.OnActiveEvent += UseItem;

            MazeAdventureItemUI itemUI = MinigameManager.Instance.CurrentMinigame.transform.Find("MinigameCanvas").Find("MinigamePanel").Find("ItemUI").GetComponent<MazeAdventureItemUI>();
            
            OnItemChange += itemUI.ChangeIcon;
            OnUseItem += itemUI.UseItemEffect;

            ChangeItem(ItemType.None);
        }

        private void OnDestroy()
        {
            input.OnActiveEvent -= UseItem;
        }

        private void UseItem()
        {
            if(currentItemType == ItemType.None) { return; }
            playerItemDictionary[currentItemType].Active();
            ChangeItem(ItemType.None);
            OnUseItem?.Invoke();
        }

        private void ChangeItem(ItemType newItem)
        {
            currentItemType = newItem;
            OnItemChange?.Invoke(newItem);
        }
        public void OnCollision(ControllerColliderHit hitInfo)
        {
            if(networkObject == null || networkObject.IsOwner == false)
                return;

            if (hitInfo.transform.TryGetComponent(out ItemBox itemBox))
            {
                if(itemBox.Alive == false)
                    return;

                OnhitItemBox?.Invoke(hitInfo.point);
                ChangeItem(itemBox.ItemType);
                OnGetItme?.Invoke();
                itemBox.Alive = false;
                MinigameManager.Instance.CurrentMinigame.DespawnMinigameObject(itemBox.NetworkObject, true);
            }
        }
    }
}
