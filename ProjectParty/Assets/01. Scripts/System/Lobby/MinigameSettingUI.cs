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
        [SerializeField] private MinigameSelectSlot minigameSelectSlotPrefab;
        [SerializeField] private Transform minigameSelectSlotContainer;
        [SerializeField] private CountText playCountText;

        public override void Init()
        {
            base.Init();

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
                MinigameSelectSlot slot = Instantiate(minigameSelectSlotPrefab, minigameSelectSlotContainer);
                slot.Init();
                slot.SetMinigameSO(minigameListSO[i]);
            }
        }
    }
}