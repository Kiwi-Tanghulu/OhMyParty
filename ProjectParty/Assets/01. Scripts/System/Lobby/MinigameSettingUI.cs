using OMG.Lobbies;
using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameSettingUI : UIPanel
    {
        [SerializeField] private MinigameListSO minigameListSO;

        [Space]
        [SerializeField] private MinigameSettingSlot minigameSelectSlotPrefab;
        [SerializeField] private Transform minigameSelectSlotContainer;
        [SerializeField] private CountText playCountText;

        private int playCount;

        private LobbyMinigameComponent minigameCompo;

        public override void Init()
        {
            base.Init();

            playCount = minigameListSO.Count;
            playCountText.Init();
            playCountText.SetCountValue(1, minigameListSO.Count);
            playCountText.SetCount(playCount, true);

            minigameCompo = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();

            CreateSlot();
        }

        public override void Hide()
        {
            base.Hide();

            CameraManager.Instance.ChangePrevCam(0f);
        }

        private void CreateSlot()
        {
            for(int i = 0; i < minigameListSO.Count; i++)
            {
                MinigameSettingSlot slot = Instantiate(minigameSelectSlotPrefab, minigameSelectSlotContainer);
                slot.OnSelectedEvent.AddListener((so, isChecked) =>
                {
                    if (isChecked)
                        minigameCompo.AddPlayAbleMinigame(so);
                    else
                    {
                        if(minigameCompo.PlayMinigameList.Count > 1)
                            minigameCompo.RemovePlayAbleMinigame(so);
                    }
                });
                slot.Init();
                slot.SetMinigameSO(minigameListSO[i]);
                slot.Select(true);
            }
        }

        public void IncreasePlayCount()
        {
            if (playCount == minigameListSO.Count)
                return;

            playCountText.SetCount(++playCount, true);
            minigameCompo.MinigameCycleCount = playCount;
        }

        public void DecreasePlayCount()
        {
            if (playCount == 1)
                return;

            playCountText.SetCount(--playCount, true);
            minigameCompo.MinigameCycleCount = playCount;
        }
    }
}