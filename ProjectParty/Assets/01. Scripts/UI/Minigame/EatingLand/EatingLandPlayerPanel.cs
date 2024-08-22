using OMG.Extensions;
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
        public override void Init(Minigame _minigame)
        {
            base.Init(_minigame);
            minigame = _minigame;
            minigame.PlayerDatas.OnListChanged += SetScore;
        }
        public void SetScore(NetworkListEvent<PlayerData> playerData)
        {
            playerSlots.ForEach((i, index) =>
            {
                bool actived = minigame.PlayerDatas.Count > index;

                if (!actived)
                    return;

                EatingLandPlayerSlot eatingLandPlayerSlot = i as EatingLandPlayerSlot;
                int playerIndex = minigame.PlayerDatas.Find(out PlayerData data, j => j.clientID == playerData.Value.clientID);
                eatingLandPlayerSlot.SetScoreText(minigame.PlayerDatas[playerIndex].score);
            });
        }
            
    }
}
