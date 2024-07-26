using UnityEngine;

namespace OMG.UI.Minigames.OhMySword
{
    public class OhMySwordPlayerSlot : ScorePlayerSlot
    {
        [SerializeField] TailPanel tailPanel = null;

        public void SetTailColor(Color color)
        {
            tailPanel.SetColor(color);
        }
    }
}