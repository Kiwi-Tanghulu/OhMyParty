namespace OMG.UI.Minigames
{
    public class ScoreTeamPanel : TeamPanel
    {
        public void SetScore(bool teamFlag, int score)
        {
            ScoreTeamSlot slot = teamSlots[teamFlag] as ScoreTeamSlot;
            if(slot == null)
                return;
                
            slot.SetScore(score);
        }
    }
}
