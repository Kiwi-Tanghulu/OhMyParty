using DG.Tweening;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using OMG.Minigames;
using OMG.NetworkEvents;
using OMG.Player.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class MinigamePickUI : UIObject
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
        [SerializeField] private float alignTime;

        [Space]
        [SerializeField] private float selectedGameShowTime = 2f;

        [Space]
        [SerializeField] private TextMeshProUGUI noticeText;
        [SerializeField] private string hostText;
        [SerializeField] private string clientText;

        [Space]
        [SerializeField] private int minSlotCount;
        [SerializeField] private int maxMinigameCount;
        private List<MinigameSlot> slotList;
        public List<MinigameSlot> SlotList => slotList;

        private Vector2 slotStartPos;
        private Vector2 slotEndPos;
        private Vector2 slotSpawnPosOffset;
        private Vector2 slotSpawnStartXPos;

        private MinigameSlot selectedSlot;
        public MinigameSlot SelectedSlot => selectedSlot;

        public UnityEvent OnStartMoveEvent;
        public UnityEvent OnStartStopMoveEvent;
        public UnityEvent OnSlotChangedEvent;
        public UnityEvent OnRouletteStopEvent;

        private NetworkEvent<IntParams> OnRouletteStopAlignEvent;

        private Action onStopAction;

        private bool isRouletteMove;

        private LobbyMinigameComponent minigameComp;

        public override void Init()
        {
            base.Init();

            minigameComp = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();

            OnRouletteStopAlignEvent = new NetworkEvent<IntParams>("OnRouletteStopAlignEvent");
            OnRouletteStopAlignEvent.AddListener(OnRouletteStopAlign);
            OnRouletteStopAlignEvent.Register(Lobby.Current.NetworkObject);

            slotStartPos = new Vector2(Rect.rect.width / 2f + slotPrefab.Rect.rect.width / 2f, 0f);
            slotEndPos = -slotStartPos;
            slotSpawnPosOffset = new Vector2(slotPrefab.Rect.rect.width + padding, 0f);
            slotSpawnStartXPos = new Vector2(-slotContainerRect.rect.width / 2f + padding, 0f);

            //make slot
            slotList = new();
            MakeSlot();

            isRouletteMove = false;
        }

        private void Update()
        {
            if (!isRouletteMove) return;

            //roulette move
            List<int> temps = new List<int>();
            for (int i = 0; i < slotList.Count; i++)
            {
                
                slotList[i].Rect.anchoredPosition += Time.deltaTime * -Vector2.right * moveSpeed;
                if (slotList[i].Rect.anchoredPosition.x <= slotEndPos.x)
                    temps.Add(i);
            }

            //teleport slot
            temps.ForEach(i =>
            {
                int frontIndex = (i - 1 + slotList.Count) % slotList.Count;
                slotList[i].Rect.anchoredPosition =
                    slotList[frontIndex].Rect.anchoredPosition + slotSpawnPosOffset;
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            //hover slot
            if (other.TryGetComponent<MinigameSlot>(out MinigameSlot slot))
            {
                selectedSlot?.Unhover();
                selectedSlot = slot;
                slot.Hover();

                OnSlotChangedEvent?.Invoke();
            }
        }

        public override void Show()
        {
            base.Show();

            SetSlotMinigameSO();

            //repositioning
            for (int i = 0; i < slotList.Count; i++)
            {
                Vector2 pos = slotSpawnStartXPos + slotSpawnPosOffset * i;

                slotList[i].Rect.anchoredPosition = pos;
            }

            StartCoroutine(MoveReady());
        }

        #region roulette
        private IEnumerator MoveReady()
        {
            InputManager.SetInputEnable(false);

            //move ready
            foreach (MinigameSlot slot in slotList)
            {
                slot.Rect.DOAnchorPosX(slot.Rect.anchoredPosition.x + readyMoveValue, readyTime);
            }

            yield return new WaitForSeconds(readyTime);

            //start delay
            yield return new WaitForSeconds(0.2f);

            moveSpeed = maxMoveSpeed;
            InputManager.SetInputEnable(true);

            //set notice text
            noticeText.text = Lobby.Current.IsHost ? hostText : clientText;

            OnStartMoveEvent?.Invoke();

            isRouletteMove = true;
        }

        public void StopRoulette(Action onStopAction)
        {
            OnStartStopMoveEvent?.Invoke();
            this.onStopAction = onStopAction;

            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => moveSpeed, x => moveSpeed = x, 0f, stopTime));
            seq.AppendCallback(() =>
            {
                isRouletteMove = false;

                if(Lobby.Current.IsServer)
                {
                    OnRouletteStopAlignEvent?.Broadcast(new IntParams(slotList.IndexOf(selectedSlot)));
                }
            });
            seq.Play();
        }

        public Sequence AlignSlotTween(int focusSlotIndex, float alignedElemShowTime
            , Action onAlignEndEvent, Action onShowEndEvent)
        {
            Sequence seq = DOTween.Sequence();

            seq.AppendCallback(() =>
            {
                //align
                foreach (MinigameSlot slot in slotList)
                {
                    MinigameSlot focusSlot = slotList[focusSlotIndex];
                    slot.Rect.DOAnchorPosX(slot.Rect.anchoredPosition.x - focusSlot.Rect.anchoredPosition.x, alignTime);
                }
            });
            seq.AppendInterval(alignTime);
            seq.AppendCallback(() => onAlignEndEvent?.Invoke());
            seq.AppendInterval(alignedElemShowTime);
            seq.AppendCallback(() => onShowEndEvent?.Invoke());

            return seq.Play();
        }

        private void OnRouletteStopAlign(IntParams param)
        {
            AlignSlotTween(param.Value, selectedGameShowTime, 
                () => OnRouletteStopEvent?.Invoke(), 
                () => onStopAction?.Invoke());
        }
        #endregion

        #region slot
        private void MakeSlot()
        {
            for (int i = 0; i < minSlotCount; i++)
            {
                Vector2 spawnPos = slotSpawnStartXPos + slotSpawnPosOffset * i;

                MinigameSlot slot = Instantiate(slotPrefab, slotContainerRect);
                slot.Init();
                slot.Rect.anchoredPosition = spawnPos;

                slotList.Add(slot);
            }
        }

        private void SetSlotMinigameSO()
        {
            List<MinigameSO> randMinigameList = minigameComp.NotPlayedMinigameList.Shuffle();
            int minigameCount = Mathf.Min(maxMinigameCount, randMinigameList.Count);

            //make slot
            for (int i = 0; i < minSlotCount; i++)
            {
                int index = i % minigameCount;

                slotList[i].SetMinigameSO(randMinigameList[index]);
            }
        }
        #endregion
    }
} 