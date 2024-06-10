namespace OMG.UI.Minigames
{
    public class DeathmatchPlayerPanel : PlayerPanel
    {
        public void SetDead(int index)
        {
            DeathmatchPlayerSlot panel = playerSlots[index] as DeathmatchPlayerSlot;
            if(panel == null)
                return;
            
            panel.SetDead(true);
        }
    }
}
