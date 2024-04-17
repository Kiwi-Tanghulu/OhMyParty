using OMG.Extensions;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyResultComponent : LobbyComponent
    {
        [SerializeField] private NetworkObject cushion;
        [SerializeField] private float cushionSpawnDelay;
        [SerializeField] private Transform[] cushionSpawnPoints;
        private WaitForSeconds cushionSpawnWFS;

        private void Awake()
        {
            cushionSpawnWFS = new WaitForSeconds(cushionSpawnDelay);
        }

        public void DisplayResult()
        {
            Lobby.PlayerDatas.ForEach(i => {
                Debug.Log($"[Lobby] Player {i.clientID} Score : {i.score}");
            });
        }

        public void DisplayWinner()
        {
            PlayerData winner = new PlayerData(0);
            Lobby.PlayerDatas.ForEach(i => {
                if(i.score >= winner.score)
                    winner = i;
            });

            Debug.Log($"[Lobby] The Winner is Player {winner.clientID}");
        }

        public void SpawnCushione()
        {
            if (!IsServer)
                return;

            StartCoroutine(SpawnCushionCo());
        }

        private IEnumerator SpawnCushionCo()
        {
            for(int i = 0; i < Lobby.PlayerDatas.Count; i++)
            {
                Vector3 spawnPoint = Lobby.Current.PlayerContainer.PlayerList[i].transform.position + Vector3.up * -1;
                //Lobby.PlayerDatas[i]
                for (int j = 0; j < 5; j++)
                {
                    NetworkObject cushion = Instantiate(this.cushion, spawnPoint, Quaternion.identity);
                    cushion.Spawn();
                    cushion.TrySetParent(gameObject);

                    yield return cushionSpawnWFS;
                }
            }
        }
    }
}
