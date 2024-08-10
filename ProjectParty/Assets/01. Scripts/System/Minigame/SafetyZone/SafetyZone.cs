using OMG.NetworkEvents;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZone : PlayableMinigame
    {
        private SafetyZoneCycle safetyZoneCycle = null;
        private SafetyTiles tiles = null;

        protected override void Awake()
        {
            base.Awake();
            tiles = GetComponent<SafetyTiles>();
        }

        protected override void OnGameInit()
        {
            base.OnGameInit();

            if(IsHost == false)
                return;

            safetyZoneCycle = cycle as SafetyZoneCycle;
            tiles.Init();
        }

        protected override void OnGameStart()
        {
            base.OnGameStart();

            if (IsHost == false)
                return;
    
            safetyZoneCycle.StartCycle(tiles.RerollTiles, tiles.DecisionSafetyZone, tiles.ResetTiles);
        }
    }
}
