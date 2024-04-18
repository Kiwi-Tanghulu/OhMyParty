namespace OMG.Minigames.SafetyZone
{
    public class SafetyZone : PlayableMinigame
    {
        private SafetyZoneCycle safetyZoneCycle = null;
        private SafetyTiles tiles = null;

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);
            safetyZoneCycle = cycle as SafetyZoneCycle;
            tiles = GetComponent<SafetyTiles>();

            for(int i = 0; i < playerDatas.Count; ++i)
                CreatePlayer(i);

            StartIntro();
        }

        public override void StartGame()
        {
            base.StartGame();

            tiles.Init();

            if (IsHost == false)
                return;
    
            safetyZoneCycle.StartCycle(tiles.RerollTiles, tiles.DecisionSafetyZone, tiles.ResetTiles);
        }

        public override void FinishGame()
        {
            base.FinishGame();
        }
    }
}
