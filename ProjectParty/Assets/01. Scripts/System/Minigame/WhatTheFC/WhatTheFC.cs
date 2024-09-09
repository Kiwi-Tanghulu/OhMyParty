using System.Collections.Generic;
using Cinemachine;
using OMG.Extensions;
using OMG.Inputs;
using OMG.NetworkEvents;
using OMG.Networks;
using OMG.UI.Minigames;
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
        [SerializeField] List<GoalPost> goalPosts = null;

        [Space(10f)]
        [SerializeField] NetworkEvent<TeamScoreParams> onGoalEvent = new NetworkEvent<TeamScoreParams>("Goal"); 
        
        private SoccerBall ball = null;
        private TeamMinigameCycle teamCycle = null;

        protected override void OnGameInit()
        {
            base.OnGameInit();

            onGoalEvent.AddListener(HandleGoal);
            onGoalEvent.Register(NetworkObject);

            teamCycle = cycle as TeamMinigameCycle;
            if(IsHost)
                teamCycle.DecideTeam();
        }

        protected override void OnGameStart()
        {
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

                goalPosts?.ForEach(i => i.Init(this));
            }

            base.OnGameStart();
        }

        protected override void OnGameFinish()
        {
            InputManager.SetInputEnable(true);
            base.OnGameFinish();
        }

        protected override void OnGameRelease()
        {
            base.OnGameRelease();

            if(IsHost)
                ball.NetworkObject.Despawn();
        }

        public void OnGoal(bool teamFlag)
        {
            int score = teamCycle.TeamTable[teamFlag].score + 1;
            teamCycle.SetTeamScore(teamFlag, score);
            onGoalEvent?.Broadcast(new TeamScoreParams(teamFlag, score));
        }

        private void HandleGoal(TeamScoreParams teamScoreParams)
        {
            Debug.Log("Goal");
            (MinigamePanel.PlayerPanel as ScoreTeamPanel).SetScore(teamScoreParams.TeamFlag, teamScoreParams.Score);
            InputManager.SetInputEnable(false);
            
            if(IsHost)
                goalPosts?.ForEach(i => i.Release());

            StartCoroutine(this.DelayCoroutine(1f, () => {
                InputManager.SetInputEnable(true);
                if(IsHost)
                    Restart();
            }));
        }

        private void Restart()
        {
            playerDatas.ForEach(i => RespawnPlayer(i.clientID));
            goalPosts?.ForEach(i => i.Init(this));
            
            ball.ResetRigidbody();
            ball.transform.position = ballPosition.position;
        }
    }
}
