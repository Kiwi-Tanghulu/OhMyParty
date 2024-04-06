using System;
using OMG.Minigames;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyMinigameComponent : LobbyComponent
    {
        [SerializeField] int playCount = 3;
        [SerializeField] Minigame[] minigames = null;

        private Minigame currentMinigame = null;

        private LobbyReadyComponent readyComponent = null;
        private int loopedCount = 0;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);
            readyComponent = Lobby.GetLobbyComponent<LobbyReadyComponent>();
            // readyComponent.OnLobbyReadyEvent += HandleLobbyReady;
        }

        private void Start()
        {
            MinigameManager.Instance.OnMinigameFinishEvent += HandleMinigameFinish;
        }

        // public void HandleLobbyReady()
        // {
        //     switch (Lobby.LobbyState)
        //     {
        //         case LobbyState.Community: // Start Minigame
        //             loopedCount = 0;
        //             Lobby.ChangeLobbyState(LobbyState.Minigame);
        //             StartMinigame();
        //             break;
        //         case LobbyState.Minigame:
        //             loopedCount++;
        //             if(loopedCount >= playCount)
        //                 return;
        //             StartMinigame();
        //             break;
        //     }
        // }

        public void StartMinigame()
        {
            readyComponent.ClearLobbyReady();
            MinigameManager.Instance.StartMinigame(minigames.PickRandom());

            Lobby.SetActive(false);
        }

        private void HandleMinigameFinish()
        {
            Lobby.SetActive(true);
        }

        public Minigame SelectMinigame()
        {
            currentMinigame = minigames.PickRandom();
            return currentMinigame;
        }
    }
}
