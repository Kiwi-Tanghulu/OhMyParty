using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventureItemUI : MonoBehaviour
    {
        [SerializeField] private GameObject textObj;
        [SerializeField] List<Sprite> itemIconList;
        [SerializeField] private Image itemImage;
        
        public void ChangeIcon(ItemType itemType)
        {
            if(itemType == ItemType.None)
            {
                itemImage.enabled = false;
                textObj.SetActive(false);
                return;
            }
            itemImage.enabled = true;
            textObj.SetActive(true);
            itemImage.sprite = itemIconList[(int)itemType-1];
        }
    }
}
