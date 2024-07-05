using OMG.Player;
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
            spawner.Release();
        }

        public override void StartGame()
        {
            base.StartGame();

            for(int i = 0; i < PlayerDatas.Count; ++i)
            {
                scoreAreas[i].SetActive(true, IsHost);
                #region 지워야 됨
                if(IsHost)
                    PlayerDictionary[playerDatas[i].clientID].GetCharacterComponent<PlayerVisual>().transform.localPosition = Vector3.zero;
                #endregion
            }

            if(IsHost == false)
                return;

            spawner.SetActive(true);
            timeAttackCycle.StartCycle(playTime);
        }

        public override void FinishGame()
        {
            for (int i = 0; i < PlayerDatas.Count; ++i)
                scoreAreas[i].SetActive(false, IsHost);

            if (IsHost)
                spawner.SetActive(false);

            base.FinishGame();
        }
    }
}
