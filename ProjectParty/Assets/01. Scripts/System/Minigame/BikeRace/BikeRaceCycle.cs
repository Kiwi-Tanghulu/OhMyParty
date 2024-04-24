using Cinemachine;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;

namespace OMG.Minigames.BikeRace
{
    public class BikeRaceCycle : TimeAttackCycle
    {
        private int goalCount;

        private BikeRace bikeRace;

        [SerializeField] private Transform[] resultStandingPoint;

        public void Init(BikeRace bikeRace)
        {
            goalCount = 0;
            
            this.bikeRace = bikeRace;
            bikeRace.OnPlayerGoal += OnPlayerGoal;
        }

        private void OnPlayerGoal(int index)
        {
            goalCount++;

            if (goalCount >= minigame.PlayerDatas.Count)
                FinishCycle();
        }

        protected override void BindingTimeLineObject(PlayableDirector timelineHolder, bool option)
        {
            if(option)
            {
                foreach (PlayableBinding binding in timelineHolder.playableAsset.outputs)
                {
                    if (binding.streamName == "Cinemachine Track")
                    {
                        timelineHolder.SetGenericBinding(binding.sourceObject, Camera.main.GetComponent<CinemachineBrain>());
                    }
                }
            }
            else
            {
                List<NetworkObject> playersSortByRank = new List<NetworkObject>();
                for (int i = 0; i < bikeRace.Rank.Count; i++)
                {
                    if (bikeRace.Players[bikeRace.Rank[i]].TryGet(out NetworkObject networkObject))
                        playersSortByRank.Add(networkObject);
                }

                for (int i = 0; i < playersSortByRank.Count; i++)
                {
                    playersSortByRank[i].GetComponent<PlayerMovement>().Teleport(resultStandingPoint[i].position);
                    playersSortByRank[i].transform.rotation = resultStandingPoint[i].rotation;

                }
            }
        }
    }
}