using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockFestival : Minigame
    {
        [SerializeField] ScoreArea[] scoreAreas = null;
        private RockSpawner spawner = null;

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            JoinedPlayers.ForEach((i, index) => {
                scoreAreas[index].Init(i.clientID);
            });

            spawner = GetComponent<RockSpawner>();
        }

        public override void StartGame()
        {
            for(int i = 0; i < JoinedPlayers.Count; ++i)
                scoreAreas[i].SetActive(true);
            spawner.SetActive(true);
        }

        public override void FinishGame()
        {
            for (int i = 0; i < JoinedPlayers.Count; ++i)
                scoreAreas[i].SetActive(true);
            spawner.SetActive(false);
        }
    }
}
