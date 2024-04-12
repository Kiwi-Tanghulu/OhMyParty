using OMG.Players;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    
    public class RockFestival : Minigame, IPlayableMinigame
    {
        [SerializeField] PlayerController playerPrefab = null;
        [SerializeField] float playTime = 60f;

        [Space(15f)]
        [SerializeField] ScoreArea[] scoreAreas = null;
        [SerializeField] Transform[] spawnPositions = null;
        
        private PlayerController[] players = null;
        public PlayerController[] Players => players;

        private RockSpawner spawner = null;
        private TimeAttackCycle timeAttackCycle = null;


        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            players = new PlayerController[playerIDs.Length];

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
        }

        public override void Release()
        {
            base.Release();
            spawner.Release();

            foreach(PlayerController player in players)
                player.NetworkObject.Despawn(true);
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
        }

        public override void FinishGame()
        {
            for (int i = 0; i < PlayerDatas.Count; ++i)
                scoreAreas[i].SetActive(false, IsHost);

            if (IsHost == false)
                return;

            spawner.SetActive(false);
            timeAttackCycle.FinishCycle();
        
            base.FinishGame();
        }

        private void CreatePlayer(int index)
        {
            Debug.Log("Client Player Setting");
            players[index] = Instantiate(playerPrefab, spawnPositions[index].position, Quaternion.identity);
            players[index].NetworkObject.SpawnWithOwnership(PlayerDatas[index].clientID, true);
            players[index].NetworkObject.TrySetParent(spawnPositions[index]);
        }
    }
}
