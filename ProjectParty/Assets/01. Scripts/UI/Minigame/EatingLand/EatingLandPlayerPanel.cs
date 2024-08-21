using OMG.UI.Minigames;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.EatingLand
{
    public class EatingLandPlayerPanel : DeathmatchPlayerPanel
    {
        private Minigame minigame = null;
        public void Init()
        {
            minigame = MinigameManager.Instance.CurrentMinigame;
            minigame.PlayerDatas.OnListChanged += SetScore;
        }
        public void SetScore(NetworkListEvent<PlayerData> playerData)
        {
            for(int i = 0; i < 4; i++)
            {
                EatingLandPlayerSlot panel = playerSlots[i] as EatingLandPlayerSlot;
                //panel.SetScoreText(minigame.PlayerDatas);
            }
        }
    }
}
