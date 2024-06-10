using UnityEngine;

namespace OMG.UI.Minigames
{
    public class ScorePlayerPanel : PlayerPanel
    {
        public void SetScore(int index, int score)
        {
            ScorePlayerSlot panel = playerSlots[index] as ScorePlayerSlot;
            if(panel == null)
                return;
            
            panel.SetScore(score);
        }
    }
}
