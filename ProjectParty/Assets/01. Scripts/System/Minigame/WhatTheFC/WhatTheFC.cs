using System.Collections.Generic;
using Cinemachine;
using OMG.Extensions;
using OMG.Networks;
using OMG.Utility;
using UnityEngine;

namespace OMG.Minigames.WhatTheFC
{
    public class WhatTheFC : PlayableMinigame
    {
        [SerializeField] OptOption<CinemachineVirtualCamera> minigameVCamOption = null;
        [SerializeField] OptOption<List<Transform>> teamPositions;

        [Space(10f)]
        [SerializeField] SoccerBall ballPrefab = null;
        [SerializeField] Transform ballPosition = null;
        private SoccerBall ball = null;

        protected override void OnGameInit()
        {
            base.OnGameInit();
            (cycle as TeamMinigameCycle).DecideTeam();
        }

        protected override void OnGameStart()
        {
            TeamMinigameCycle teamCycle = cycle as TeamMinigameCycle;
            bool teamFlag = teamCycle.TeamInfo[ClientManager.Instance.ClientID];
            minigameVCamOption[teamFlag].Priority = DEFINE.FOCUSED_PRIORITY;
            minigameVCamOption[!teamFlag].Priority = DEFINE.UNFOCUSED_PRIORITY;

            if(IsHost)
            {
                spawnPositions = new List<Transform>();
                PlayerDatas.ForEach((i, index) => {
                    bool teamFlag = teamCycle.TeamInfo[i.clientID];
                    spawnPositions.Add(teamPositions[teamFlag][0]);
                    teamPositions[teamFlag].RemoveAt(0);
                });

                ball = Instantiate(ballPrefab, ballPosition.position, Quaternion.identity);
                ball.NetworkObject.Spawn();
            }

            base.OnGameStart();
        }

        protected override void OnGameRelease()
        {
            base.OnGameRelease();

            if(IsHost)
                ball.NetworkObject.Despawn();
        }
    }
}
