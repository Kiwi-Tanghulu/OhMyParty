using UnityEngine;
using OMG.UI;
using OMG.Minigames;
using OMG.Minigames.Race;
using System;

namespace OMG.Player
{
    public class ObstacleRunPlayerController : PlayerController
    {
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Race race = MinigameManager.Instance.CurrentMinigame as Race;

            race.OnReadyGame += Race_OnReadyGame;
            race.OnStartGame += Race_OnStartGame;
        }

        private void Race_OnReadyGame()
        {
            StateMachine.ChangeState("ReadyState");
        }

        private void Race_OnStartGame()
        {
            StateMachine.ChangeState("RunState");
        }

        public void HangingObstacle()
        {
            StateMachine.ChangeState("StunState");
        }
    }
}