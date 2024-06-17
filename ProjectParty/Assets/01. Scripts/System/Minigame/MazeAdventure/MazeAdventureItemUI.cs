using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventureItemUI : MonoBehaviour
    {
        [SerializeField] private GameObject textObj;
        [SerializeField] List<Sprite> itemIconList;
        [SerializeField] private Image itemImage;
        [SerializeField] private RectTransform itemRectTrm;

        private CanvasGroup itemCanvasGroup;

        private void Awake()
        {
            itemCanvasGroup = itemRectTrm.GetComponent<CanvasGroup>();
        }
        public void ChangeIcon(ItemType itemType)
        {
            if(itemType == ItemType.None)
            {
                itemImage.enabled = false;
                textObj.SetActive(false);
                return;
            }
            itemRectTrm.DOScale(1f, 0f);
            itemCanvasGroup.DOFade(1f, 0f);
            itemImage.enabled = true;
            textObj.SetActive(true);
            itemImage.sprite = itemIconList[(int)itemType-1];
        }

        public void UseItemEffect()
        {
            // 크기 애니메이션
            itemRectTrm.DOScale(1.3f, 1f).SetEase(Ease.OutQuart);

            // 알파값 애니메이션
            if (itemCanvasGroup == null)
            {
                itemCanvasGroup = itemRectTrm.gameObject.AddComponent<CanvasGroup>();
            }
            itemCanvasGroup.DOFade(0f, 1f).SetEase(Ease.OutQuart);
        }
    }
}
