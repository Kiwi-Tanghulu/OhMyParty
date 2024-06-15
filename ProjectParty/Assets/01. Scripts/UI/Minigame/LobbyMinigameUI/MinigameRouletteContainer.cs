using DG.Tweening;
using OMG.Inputs;
using OMG.Lobbies;
using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameRouletteContainer : UIObject
    {
        [SerializeField] private MinigameListSO minigameListSO;

        [Space]
        [SerializeField] private RectTransform slotContainerRect;
        [SerializeField] private MinigameSlot slotPrefab;
        [SerializeField] private float padding;
        [SerializeField] private float moveSpeed;

        [Space]
        [SerializeField] private float readyTime;
        [SerializeField] private float readyMoveValue;

        private List<MinigameSlot> slotList;

        private Vector2 slotStartPos;
        private Vector2 slotEndPos;
        private float slotSpawnPosOffset;
        private float slotSpawnStartXPos;

        private bool isRouletteMove;

        private MinigameSO selectedMinigame;
        public MinigameSO SelectedMinigame => selectedMinigame;

        public override void Init()
        {
            base.Init();

            slotList = new();

            slotStartPos = new Vector2(Rect.rect.width / 2f + slotPrefab.Rect.rect.width / 2f, 0f);
            slotEndPos = -slotStartPos;
            slotSpawnPosOffset = slotPrefab.Rect.rect.width + padding;
            slotSpawnStartXPos = -slotContainerRect.rect.width / 2f + padding;
        }

        private void Update()
        {
            if(isRouletteMove)
            {
                foreach(MinigameSlot slot in slotList)
                {
                    //move
                    slot.Rect.anchoredPosition += -Vector2.right * moveSpeed;

                    //teleport slot
                    if (slot.Rect.anchoredPosition.x < slotEndPos.x)
                        slot.Rect.anchoredPosition = slotStartPos;
                }
            }
        }

        public override void Show()
        {
            base.Show();

            foreach(MinigameSlot slot in slotList)
            {
                slotList.Remove(slot);
                Destroy(slot.gameObject);
            }

            for (int i = 0; i < minigameListSO.Count; i++)
            {
                Vector2 spawnPos = new Vector2(slotSpawnStartXPos + (slotSpawnPosOffset * i), 0);
                
                MinigameSlot slot = Instantiate(slotPrefab, slotContainerRect);
                slot.Init();
                slot.SetMinigameSO(minigameListSO[i]);
                slot.Rect.anchoredPosition = spawnPos;

                slotList.Add(slot);
            }

            StartCoroutine(MoveReady());
        }

        private IEnumerator MoveReady()
        {
            InputManager.SetInputEnable(false);

            foreach(MinigameSlot slot in slotList)
            {
                slot.Rect.DOAnchorPosX(slot.Rect.anchoredPosition.x + readyMoveValue, readyTime);
            }

            yield return new WaitForSeconds(readyTime);

            isRouletteMove = true;
            InputManager.SetInputEnable(true);
        }

        public void StopRoulette()
        {
            isRouletteMove = false;

            float minDist = Mathf.Infinity;
            MinigameSlot currentSlot = null;

            for(int i = 0; i < slotList.Count; i++)
            {
                if (Mathf.Abs(slotList[i].Rect.anchoredPosition.x - slotContainerRect.anchoredPosition.x) < minDist)
                    currentSlot = slotList[i];
            }
            selectedMinigame = currentSlot.MinigameSO;
        }
    }
}