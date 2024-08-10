using OMG.Minigames.RunAway;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class ObstacleRun : PlayableMinigame
    {
        private RaceCycle raceCycle;

        //[SerializeField] private PlayerItemBox itemBoxPrefab;
        //[SerializeField] private Transform itemBoxSpawnPoints;

        protected override void OnGameInit()
        {
            base.OnGameInit();

            raceCycle = cycle as RaceCycle;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            //foreach(Transform point in itemBoxSpawnPoints)
            //{
            //    PlayerItemBox itemBox = Instantiate(itemBoxPrefab, point.position, point.rotation);
            //    itemBox.NetworkObject.Spawn();
            //    itemBox.NetworkObject.TrySetParent(transform);
            //}
        }
    }
}