using Cinemachine;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;

namespace OMG.Minigames.BikeRace
{
    public class RaceCycle : TimeAttackCycle
    {
        private int goalCount;

        private Race bikeRace;

        [SerializeField] private PlayerVisual[] resultPlayerVisuals;

        protected override void Awake()
        {
            base.Awake();

            goalCount = 0;
            bikeRace = minigame as Race;

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
            foreach (PlayableBinding binding in timelineHolder.playableAsset.outputs)
            {
                if (binding.streamName == "Cinemachine Track")
                {
                    timelineHolder.SetGenericBinding(binding.sourceObject, Camera.main.GetComponent<CinemachineBrain>());
                }
            }

            if (option)
            {
                
            }
            else
            {
                List<PlayerController> playersSortByRank = new List<PlayerController>();
                
                for (int i = 0; i < bikeRace.Rank.Count; i++)
                {
                    if (bikeRace.Players[bikeRace.Rank[i]].TryGet(out NetworkObject networkObject))
                        playersSortByRank.Add(networkObject.GetComponent<PlayerController>());
                }

                for (int i = 0; i < playersSortByRank.Count; i++)
                {
                    resultPlayerVisuals[i].SetVisual(playersSortByRank[i].Visual.VisualType); 
                }
            }
        }
    }
}