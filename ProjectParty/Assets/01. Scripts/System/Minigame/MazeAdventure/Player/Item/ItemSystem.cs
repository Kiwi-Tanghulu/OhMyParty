using OMG.Inputs;
using OMG.Items;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
public class ItemSystem : MonoBehaviour, IPlayerCollision
    {
        [SerializeField] private PlayInputSO input;
        [SerializeField] private ParticleSystem itemHitParticle;
        private Dictionary<ItemType, MazeAdventureItem> playerItemDictionary = null;
        private ItemType currentItemType;
        public void Init(Transform playerTrm)
        {
            currentItemType = ItemType.None;
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
            currentItemType = ItemType.None;
        }

        private void ChangeItem(ItemType newItem)
        {
            currentItemType = newItem;
        }

        //private void OnControllerColliderHit(ControllerColliderHit hit)
        //{
        //    if(hit.transform.TryGetComponent(out ItemBox itemBox))
        //    {
        //        ChangeItem(itemBox.ItemType);
        //        Destroy(itemBox.gameObject);
        //    }
        //}
        public void OnCollision(ControllerColliderHit hitInfo)
        {
            if (hitInfo.transform.TryGetComponent(out ItemBox itemBox))
            {
                Instantiate(itemHitParticle, hitInfo.point,Quaternion.identity);
                ChangeItem(itemBox.ItemType);

                itemBox.transform.GetComponent<NetworkObject>().Despawn(true);
            }
        }
    }
}
