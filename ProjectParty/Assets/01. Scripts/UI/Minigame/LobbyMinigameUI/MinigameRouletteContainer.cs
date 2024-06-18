using DG.Tweening;
using OMG.Inputs;
using OMG.Minigames;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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

        [Space]
        [SerializeField] private float selectedGameShowTime = 2f;

        private List<MinigameSlot> slotList;
        public List<MinigameSlot> SlotList => slotList;

        private Vector2 slotStartPos;
        private Vector2 slotEndPos;
        private Vector2 slotSpawnPosOffset;
        private Vector2 slotSpawnStartXPos;

        private MinigameSlot selectedSlot;
        public MinigameSlot SelectedSlot => selectedSlot;
        public MinigameSO SelectedMinigame => selectedSlot?.MinigameSO;

        public UnityEvent OnStartMoveEvent;
        public UnityEvent OnStartStopMoveEvent;
        public UnityEvent OnSlotChangedEvent;
        public UnityEvent OnRouletteStopEvent;

        private Action onRouletteStopAction;

        private bool isRouletteMove;

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

            isRouletteMove = false;
        }

        private void Update()
        {
            if (!isRouletteMove) return;

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
                selectedSlot?.Deselected();
                selectedSlot = slot;
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

            moveSpeed = maxMoveSpeed;
            InputManager.SetInputEnable(true);

            OnStartMoveEvent?.Invoke();

            isRouletteMove = true;
        }

        public void StopRoulette(/*Action onStopAction*/)
        {
            OnStartStopMoveEvent?.Invoke();
            //onRouletteStopAction = onStopAction;

            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => moveSpeed, x => moveSpeed = x, 0f, stopTime));
            seq.AppendCallback(() =>
            {
                isRouletteMove = false;
                OnRouletteStopEvent?.Invoke();
                //AlignSlotClientRpc(slotList.IndexOf(selectedSlot));
            });
            //seq.AppendInterval(selectedGameShowTime);
            //seq.AppendCallback(() => onStopAction?.Invoke());
            seq.Play();
        }

        public Sequence AlignSlotTween(int focusSlotIndex)
        {
            Sequence seq = DOTween.Sequence();

            seq.AppendCallback(() =>
            {
                //align
                foreach (MinigameSlot slot in slotList)
                {
                    MinigameSlot focusSlot = slotList[focusSlotIndex];
                    slot.Rect.DOAnchorPosX(slot.Rect.anchoredPosition.x - focusSlot.Rect.anchoredPosition.x, readyTime);
                }
            });

            return seq;
            //seq.AppendInterval(selectedGameShowTime);
            //seq.AppendCallback(() => onRouletteStopAction?.Invoke());
            //return seq.Play();
        }
    }
}