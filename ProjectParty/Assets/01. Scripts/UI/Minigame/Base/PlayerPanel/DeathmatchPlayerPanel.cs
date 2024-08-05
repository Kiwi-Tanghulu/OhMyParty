namespace OMG.UI.Minigames
{
    public class DeathmatchPlayerPanel : PlayerPanel
    {
        public void SetDead(int index, bool isDead)
        {
            DeathmatchPlayerSlot panel = playerSlots[index] as DeathmatchPlayerSlot;
            if(panel == null)
                return;
            
            panel.SetDead(isDead);
        }

        public void SetDead(int index, int count)
        {
            DeathmatchPlayerSlot panel = playerSlots[index] as DeathmatchPlayerSlot;
            if(panel == null)
                return;

            panel.SetDead(count);
        }
    }
}
