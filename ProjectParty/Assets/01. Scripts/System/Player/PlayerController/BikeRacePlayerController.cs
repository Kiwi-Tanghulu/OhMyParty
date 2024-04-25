using Cinemachine;
using JetBrains.Annotations;
using OMG.Input;
using OMG.Minigames;
using OMG.Minigames.BikeRace;
using OMG.Player.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Player
{
    public class BikeRacePlayerController : PlayerController
    {
        public event Action OnContectGround;
        public UnityEvent OnGoal;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            BikeRace bikeRace = MinigameManager.Instance.CurrentMinigame as BikeRace;
            Debug.Log(MinigameManager.Instance);
            Debug.Log(MinigameManager.Instance.CurrentMinigame);
            Debug.Log(bikeRace);

            if (IsOwner)
            {
                bikeRace.OnStartGame += Minigame_OnStartGame;
            }
        }

        public void Goal()
        {
            OnGoal?.Invoke();
            gameObject.SetActive(false);
        }

        private void Minigame_OnStartGame()
        {
            Debug.Log(123);
            StateMachine.ChangeState(typeof(BikeMoveState));
        }
    }
}