using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class RunAwayPlayerPanel : RacePlayerPanel
    {
        public void SetDead(int index)
        {
            RunAwayPlayerSlot panel = playerSlots[index] as RunAwayPlayerSlot;
            if (panel == null)
                return;

            panel.SetDead(true);
        }
    }
}