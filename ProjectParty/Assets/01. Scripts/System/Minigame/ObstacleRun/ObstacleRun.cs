using OMG.Minigames.RunAway;
using OMG.NetworkEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class ObstacleRun : PlayableMinigame
    {
        private RaceCycle raceCycle;

        [SerializeField] private float startSpectateDelay;

        //[SerializeField] private PlayerItemBox itemBoxPrefab;
        //[SerializeField] private Transform itemBoxSpawnPoints;

        protected override void OnGameInit()
        {
            base.OnGameInit();

            raceCycle = cycle as RaceCycle;

            raceCycle.onPlayerGoalEndEvent.AddListener(HandlePlayerGoalEnd);
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

        private void HandlePlayerGoalEnd(UlongParams clientID)
        {
            if(NetworkManager.LocalClientId == clientID.Value)
            {
                if(TryGetComponent<MinigameSpectate>(out MinigameSpectate spectate))
                {
                    spectate.StartSpectate();
                }
            }
        }
    }
}