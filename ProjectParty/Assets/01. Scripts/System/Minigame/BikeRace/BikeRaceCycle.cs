using Cinemachine;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Playables;

namespace OMG.Minigames.BikeRace
{
    public class BikeRaceCycle : TimeAttackCycle
    {
        private int goalCount;

        public void Init(BikeRace bikeRace)
        {
            goalCount = 0;
            
            bikeRace.OnPlayerGoal += OnPlayerGoal;
        }

        private void OnPlayerGoal(int index)
        {
            goalCount++;

            if (goalCount >= minigame.PlayerDatas.Count)
                FinishCycle();
        }

        protected override void BindingTimeLineObject(PlayableDirector timelineHolder)
        {
            foreach (PlayableBinding binding in timelineHolder.playableAsset.outputs)
            {
                if (binding.streamName == "Cinemachine Track")
                {
                    timelineHolder.SetGenericBinding(binding.sourceObject, Camera.main.GetComponent<CinemachineBrain>()); 
                }
            }
        }
    }
}