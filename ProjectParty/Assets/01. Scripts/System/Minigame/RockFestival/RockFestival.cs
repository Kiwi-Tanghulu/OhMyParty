using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockFestival : Minigame
    {
        [SerializeField] float playTime = 60f;

        [Space(15f)]
        [SerializeField] ScoreArea[] scoreAreas = null;

        private RockSpawner spawner = null;
        private TimeAttackCycle timeAttackCycle = null;

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            for(int i = 0; i < scoreAreas.Length; ++i)
            {
                if(i >= JoinedPlayers.Count)
                    scoreAreas[i].Init(0, false);
                else
                    scoreAreas[i].Init(JoinedPlayers[i].clientID, true);
            }

            spawner = GetComponent<RockSpawner>();
            timeAttackCycle = cycle as TimeAttackCycle;

            timeAttackCycle.SetPlayTime(playTime);
        }

        public override void Release()
        {
            spawner.Release();
        }

        public override void StartGame()
        {
            for(int i = 0; i < JoinedPlayers.Count; ++i)
                scoreAreas[i].SetActive(true, IsHost);

            if(IsHost == false)
                return;

            spawner.SetActive(true);
            timeAttackCycle.StartCycle();
        }

        public override void FinishGame()
        {
            for (int i = 0; i < JoinedPlayers.Count; ++i)
                scoreAreas[i].SetActive(false, IsHost);

            if (IsHost == false)
                return;

            spawner.SetActive(false);
            timeAttackCycle.FinishCycle();
        
            base.FinishGame();
        }
    }
}
