using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class RacePlayerPanel : PlayerPanel
    {
        public void SetGoal(int index)
        {
            RacePlayerSlot panel = playerSlots[index] as RacePlayerSlot;
            if (panel == null)
                return;

            panel.SetGoal(true);
        }
    }
}