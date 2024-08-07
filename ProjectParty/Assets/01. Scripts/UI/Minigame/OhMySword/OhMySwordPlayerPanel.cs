using UnityEngine;

namespace OMG.UI.Minigames.OhMySword
{
    public class OhMySwordPlayerPanel : ScorePlayerPanel
    {
        [SerializeField] TailPanel tailPanel = null;

        public void SetTargetTailColor(Color color)
        {
            tailPanel.SetColor(color);
        }

        // public void SetTargetTailColor(int index, Color color)
        // {
        //     OhMySwordPlayerSlot panel = playerSlots[index] as OhMySwordPlayerSlot;
        //     panel.SetTailColor(color);
        // }
    }
}
