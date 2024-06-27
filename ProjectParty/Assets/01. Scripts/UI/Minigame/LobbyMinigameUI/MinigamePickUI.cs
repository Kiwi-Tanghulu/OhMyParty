using Cinemachine;
using DG.Tweening;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using OMG.Minigames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace OMG.UI
{
    public class MinigamePickUI : UIObject
    {
        [SerializeField] private MinigameListSO minigameListSO;

        [Space]
        [SerializeField] private RectTransform slotContainerRect;
        [SerializeField] private MinigameSlot slotPrefab;
        [SerializeField] private float padding;

        [Space]
        [SerializeField] private float createSlotDelay;
        [SerializeField] private float waitSlotShowTime;
        [SerializeField] private float waitHideSlotContentTime;
        [SerializeField] private float alignSlotTime;
        [SerializeField] private float slotShuffleTime;
        [SerializeField] private float showInfoUIDelayTime;
        private Vector2 slotSpawnPosOffset;

        [Space]
        [SerializeField] private TextMeshProUGUI noticeText;

        [Space]
        [SerializeField] private int slotCount = 3;

        private List<MinigameSlot> slotList;
        public List<MinigameSlot> SlotList => slotList;

        private MinigameSO selectedMinigame;

        private LobbyMinigameComponent lobbyMinigameComponent;

        public override void Init()
        {
            base.Init();

            slotSpawnPosOffset = new Vector2(slotPrefab.Rect.rect.width + padding, 0f);
            slotList = new List<MinigameSlot>();

            lobbyMinigameComponent = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
        }

        public override void Show()
        {
            base.Show();

            StartCoroutine(CreaetSlotSequence());
        }

        public override void Hide()
        {
            StopAllCoroutines();

            base.Hide();
        }

        private IEnumerator CreaetSlotSequence()
        {
            yield return StartCoroutine(CreateSlot());

            yield return new WaitForSeconds(waitSlotShowTime);

            yield return StartCoroutine(HideSlotContent());

            yield return new WaitForSeconds(waitHideSlotContentTime);

            CollectSlot();
            yield return new WaitForSeconds(alignSlotTime);

            yield return new WaitForSeconds(slotShuffleTime);

            AlignSlot();

            yield return new WaitForSeconds(alignSlotTime);

            for (int i = 0; i < slotCount; i++)
                slotList[i].IsSelectable = true;

            noticeText.gameObject.SetActive(true);
        }

        private IEnumerator CreateSlot()
        {
            List<MinigameSO> shuffledMinigameList = minigameListSO.MinigameList.Shuffle();

            for(int i = slotList.Count - 1; i >= 0; i--)
            {
                Destroy(slotList[i]?.gameObject);
                slotList.RemoveAt(i);
            }

            for (int i = 0; i < slotCount; i++)
            {
                Vector2 spawnPos = slotContainerRect.anchoredPosition + slotSpawnPosOffset * (i - slotCount / 2);

                MinigameSlot slot = Instantiate(slotPrefab, slotContainerRect);
                slot.Init();
                slot.SetMinigameSO(shuffledMinigameList[i]);
                slot.OnSelectedEvent.AddListener(MinigameSlot_OnSelected);
                slot.Rect.anchoredPosition = spawnPos;
                slot.IsSelectable = false;
                slot.Show();

                slotList.Add(slot);

                yield return new WaitForSeconds(createSlotDelay);
            }
        }

        private IEnumerator HideSlotContent()
        {
            for (int i = 0; i < slotCount; i++)
            {
                slotList[i].ShowContent(false);

                yield return new WaitForSeconds(createSlotDelay);
            }
        }

        private void CollectSlot()
        {
            for (int i = 0; i < slotCount; i++)
            {
                slotList[i].Rect.DOAnchorPos(slotContainerRect.anchoredPosition, alignSlotTime);
            }
        }

        private void AlignSlot()
        {
            for (int i = 0; i < slotCount; i++)
            {
                Vector2 alignPos = slotContainerRect.anchoredPosition + slotSpawnPosOffset * (i - slotCount / 2);

                slotList[i].Rect.DOAnchorPos(alignPos, alignSlotTime);
            }
        }

        private void MinigameSlot_OnSelected(MinigameSlot slot)
        {
            slot.SetMinigameSO(slotList[0].MinigameSO);

            slot.ShowContent(true);
            slot.OnSelectedEvent.RemoveListener(MinigameSlot_OnSelected);
            
            StartCoroutine(this.DelayCoroutine(showInfoUIDelayTime,
                () => lobbyMinigameComponent.SelectMinigame(slotList[0].MinigameSO)));
        }
    }
}