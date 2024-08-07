using OMG.UI.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.EatingLand
{
    public class EatingLandPlayerPanel : DeathmatchPlayerPanel
    {
        public void SetScore(int index, int score)
        {
            EatingLandPlayerSlot panel = playerSlots[index] as EatingLandPlayerSlot;
            if (panel == null)
                return;

            panel.SetScore(score);
        }
    }
}
