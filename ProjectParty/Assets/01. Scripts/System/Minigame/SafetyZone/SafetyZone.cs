using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZone : PlayableMinigame
    {
        [SerializeField] SafetyTile[] tiles = null;
        private SafetyZoneCycle safetyZoneCycle = null;

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);
            safetyZoneCycle = cycle as SafetyZoneCycle;
        }

        public override void StartGame()
        {
            base.StartGame();

            if (IsHost == false)
                return;

            safetyZoneCycle.StartCycle(RerollTiles, DecisionSafetyZone);
        }

        public override void FinishGame()
        {
            if (IsHost)
            {
                safetyZoneCycle.FinishCycle();
            }

            base.FinishGame();
        }

        private void RerollTiles()
        {
            Debug.Log("Reroll Tile");
        }

        private void DecisionSafetyZone()
        {
            Debug.Log("Decision Safety Zone");
        }
    }
}
