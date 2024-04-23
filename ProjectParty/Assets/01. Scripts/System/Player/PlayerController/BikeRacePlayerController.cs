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

            if(IsHost)
            {
                BikeRace bikeRace = MinigameManager.Instance.CurrentMinigame as BikeRace;
                bikeRace.OnStartGame += Minigame_OnStartGame;

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Ground"))
            {
                //OnContectGround?.Invoke();
                Debug.Log("on contect ground");
            }
        }

        public void Goal()
        {
            OnGoal?.Invoke();
        }

        private void Minigame_OnStartGame()
        {
            StateMachine.ChangeState(typeof(BikeMoveState));
        }
    }
}