using DG.Tweening;
using OMG.Inputs;
using OMG.Minigames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class MinigameRouletteContainer : UIObject
    {
        [SerializeField] private MinigameListSO minigameListSO;

        [Space]
        [SerializeField] private RectTransform slotContainerRect;
        [SerializeField] private MinigameSlot slotPrefab;
        [SerializeField] private float padding;
        [SerializeField] private float maxMoveSpeed;
        [SerializeField] private float stopTime;
        private float moveSpeed;

        [Space]
        [SerializeField] private float readyTime;
        [SerializeField] private float readyMoveValue;

        private List<MinigameSlot> slotList;

        private Vector2 slotStartPos;
        private Vector2 slotEndPos;
        private Vector2 slotSpawnPosOffset;
        private Vector2 slotSpawnStartXPos;

        private bool isRouletteMove;

        private MinigameSlot selctedSlot;
        public MinigameSO SelectedMinigame => selctedSlot?.MinigameSO;

        public UnityEvent OnStartMoveEvent;
        public UnityEvent OnStartStopMoveEvent;
        public UnityEvent OnSlotChangedEvent;

        public override void Init()
        {
            base.Init();


            slotStartPos = new Vector2(Rect.rect.width / 2f + slotPrefab.Rect.rect.width / 2f, 0f);
            slotEndPos = -slotStartPos;
            slotSpawnPosOffset = new Vector2(slotPrefab.Rect.rect.width + padding, 0f);
            slotSpawnStartXPos = new Vector2(-slotContainerRect.rect.width / 2f + padding, 0f);

            //make slot
            slotList = new();
            for (int i = 0; i < minigameListSO.Count; i++)
            {
                Vector2 spawnPos = slotSpawnStartXPos + slotSpawnPosOffset * i;

                MinigameSlot slot = Instantiate(slotPrefab, slotContainerRect);
                slot.Init();
                slot.SetMinigameSO(minigameListSO[i]);
                slot.Rect.anchoredPosition = spawnPos;

                slotList.Add(slot);
            }
        }

        private void Update()
        {
            //roulette move
            for(int i = 0; i < slotList.Count; i++)
            {
                slotList[i].Rect.anchoredPosition += Time.deltaTime * -Vector2.right * moveSpeed;

                //teleport slot
                if (slotList[i].Rect.anchoredPosition.x < slotEndPos.x)
                {
                    int frontIndex = i == 0 ? slotList.Count - 1 : i - 1;
                    slotList[i].Rect.anchoredPosition = 
                        slotList[frontIndex].Rect.anchoredPosition + slotSpawnPosOffset;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<MinigameSlot>(out MinigameSlot slot))
            {
                selctedSlot?.Deselected();
                selctedSlot = slot;
                slot.Selected();

                OnSlotChangedEvent?.Invoke();
            }
        }

        public override void Show()
        {
            base.Show();

            //repositioning
            for (int i = 0; i < slotList.Count; i++)
            {
                Vector2 pos = slotSpawnStartXPos + slotSpawnPosOffset * i;

                slotList[i].Rect.anchoredPosition = pos;
            }

            StartCoroutine(MoveReady());
        }

        private IEnumerator MoveReady()
        {
            InputManager.SetInputEnable(false);

            //move ready
            foreach(MinigameSlot slot in slotList)
            {
                slot.Rect.DOAnchorPosX(slot.Rect.anchoredPosition.x + readyMoveValue, readyTime);
            }

            yield return new WaitForSeconds(readyTime);
            
            //start delay
            yield return new WaitForSeconds(0.2f);

            isRouletteMove = true;
            moveSpeed = maxMoveSpeed;
            InputManager.SetInputEnable(true);

            OnStartMoveEvent?.Invoke();
        }

        public void StopRoulette(Action onStopAction)
        {
            isRouletteMove = false;

            DOTween.To(() => moveSpeed, x => moveSpeed = x, 0f, stopTime)
                .OnComplete(() => onStopAction?.Invoke());

            OnStartStopMoveEvent?.Invoke();
        }
    }
}