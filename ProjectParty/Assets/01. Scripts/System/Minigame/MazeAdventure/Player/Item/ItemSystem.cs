using OMG.Input;
using OMG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace OMG.Minigames.MazeAdventure
{
    public enum ItemType
    {
        None,
        MakeWall,
        Invisibility,
    }
public class ItemSystem : MonoBehaviour
    {
        [SerializeField] private PlayInputSO input;
        private Dictionary<ItemType, MazeAdventureItem> playerItemDictionary = null;
        private ItemType currentItemType;
        public void Init(Transform playerTrm)
        {
            currentItemType = ItemType.Invisibility;
            playerItemDictionary = new Dictionary<ItemType, MazeAdventureItem>();
            foreach(Transform itemTrm in transform)
            {
                if(itemTrm.TryGetComponent<MazeAdventureItem>(out MazeAdventureItem item))
                {
                    item.Init(playerTrm);
                    playerItemDictionary.Add(item.ItemType, item);
                }
            }

            input.OnActiveEvent += UseItem;
        }

        private void UseItem()
        {
            if(currentItemType == ItemType.None) { return; }
            playerItemDictionary[currentItemType].Active();
            //currentItemType = ItemType.None;
        }
    }
}
