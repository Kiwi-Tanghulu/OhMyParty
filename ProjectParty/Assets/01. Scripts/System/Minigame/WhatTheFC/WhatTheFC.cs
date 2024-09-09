using Cinemachine;
using OMG.Networks;
using OMG.Utility;
using UnityEngine;

namespace OMG.Minigames.WhatTheFC
{
    public class WhatTheFC : PlayableMinigame
    {
        [SerializeField] OptOption<CinemachineVirtualCamera> minigameVCamOption = null;

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
            base.OnGameStart();

            if(IsHost)
            {
                ball = Instantiate(ballPrefab, ballPosition.position, Quaternion.identity);
                ball.NetworkObject.Spawn();
            }

            bool teamFlag = (cycle as TeamMinigameCycle).TeamInfo[ClientManager.Instance.ClientID];
            minigameVCamOption[teamFlag].Priority = DEFINE.FOCUSED_PRIORITY;
            minigameVCamOption[!teamFlag].Priority = DEFINE.UNFOCUSED_PRIORITY;
        }

        protected override void OnGameRelease()
        {
            base.OnGameRelease();

            if(IsHost)
                ball.NetworkObject.Despawn();
        }
    }
}
