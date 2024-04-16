using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockFestival : PlayableMinigame
    {
        [SerializeField] ScoreArea[] scoreAreas = null;

        [Space(15f)]
        [SerializeField] float playTime = 60f;

        private RockSpawner spawner = null;
        private TimeAttackCycle timeAttackCycle = null;

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            for(int i = 0; i < scoreAreas.Length; ++i)
            {
                if(i >= PlayerDatas.Count)
                    scoreAreas[i].Init(0, false);
                else
                {
                    scoreAreas[i].Init(PlayerDatas[i].clientID, true);
                    CreatePlayer(i);
                }
            }

            spawner = GetComponent<RockSpawner>();
            timeAttackCycle = cycle as TimeAttackCycle;

            timeAttackCycle.SetPlayTime(playTime);

            StartIntro();
        }

        public override void Release()
        {
            base.Release();
            spawner.Release();
        }

        public override void StartGame()
        {
            base.StartGame();

            for(int i = 0; i < PlayerDatas.Count; ++i)
                scoreAreas[i].SetActive(true, IsHost);

            if(IsHost == false)
                return;

            spawner.SetActive(true);
            timeAttackCycle.StartCycle();
            minigameUI.ScorePanel.Display(true);
        }

        public override void FinishGame()
        {
            for (int i = 0; i < PlayerDatas.Count; ++i)
                scoreAreas[i].SetActive(false, IsHost);

            if (IsHost)
                spawner.SetActive(false);

            minigameUI.ScorePanel.Display(false);
            base.FinishGame();
        }
    }
}
