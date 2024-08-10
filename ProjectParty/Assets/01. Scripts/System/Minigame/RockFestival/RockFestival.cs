using OMG.NetworkEvents;
using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockFestival : PlayableMinigame
    {
        [SerializeField] ScoreArea[] scoreAreas = null;

        private RockSpawner spawner = null;
        private TimeAttackCycle timeAttackCycle = null;

        public override void Init()
        {
            base.Init();

            if(IsHost == false)
                return;

            for(int i = 0; i < scoreAreas.Length; ++i)
            {
                if(i >= PlayerDatas.Count)
                    scoreAreas[i].Init(0, false);
                else
                    scoreAreas[i].Init(PlayerDatas[i].clientID, true);
            }

            spawner = GetComponent<RockSpawner>();
            timeAttackCycle = cycle as TimeAttackCycle;
        }

        public override void Release()
        {
            base.Release();

            if(IsHost == false)
                return;

            spawner.Release();
        }

        protected override void OnGameStart(NoneParams ignore)
        {
            base.OnGameStart(ignore);

            for(int i = 0; i < PlayerDatas.Count; ++i)
                scoreAreas[i].SetActive(true, IsHost);

            if(IsHost == false)
                return;

            spawner.SetActive(true);
            timeAttackCycle.StartCycle();
        }

        protected override void OnGameFinish(NoneParams ignore)
        {
            for (int i = 0; i < PlayerDatas.Count; ++i)
                scoreAreas[i].SetActive(false, IsHost);

            if (IsHost)
                spawner.SetActive(false);

            base.OnGameFinish(ignore);
        }
    }
}
