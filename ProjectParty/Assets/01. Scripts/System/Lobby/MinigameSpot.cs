using System;
using Cinemachine;
using OMG.Input;
using OMG.Interacting;
using OMG.Minigames;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class MinigameSpot : NetworkBehaviour, IInteractable
    {
        [SerializeField] UIInputSO input = null;
        [SerializeField] MinigameListSO minigameList = null;

        private CinemachineVirtualCamera focusVCam = null;
        private LobbyMinigameComponent minigameComponent = null;
        private LobbyResultComponent resultComponent = null;
        private LobbyReadyComponent readyComponent = null;

        private void Awake()
        {
            focusVCam = transform.Find("FocusVCam").GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            minigameComponent = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            resultComponent = Lobby.Current.GetLobbyComponent<LobbyResultComponent>();
            readyComponent = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();

            minigameComponent.OnMinigameSelectedEvent += HandleMinigameSelected;
            minigameComponent.OnMinigameFinishedEvent += HandleMinigameFinished;
            readyComponent.OnLobbyReadyEvent += HandleLobbyReady;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsHost)
                input.OnSpaceEvent += HandleSpaceInput;
        }

        public bool Interact(Component performer, bool actived, Vector3 point = default)
        {
            if(actived == false)
                return false;

            readyComponent.Ready();
            return true;
        }

        private void HandleLobbyReady()
        {
            switch(Lobby.Current.LobbyState)
            {
                case LobbyState.Community: // 커뮤니티 상태일 때 레디가 되면 미니게임 선택 시작
                case LobbyState.MinigameFinished: // 그 전 미니게임이 끝난 상태일 때도 마찬가지
                    if(IsHost)
                        minigameComponent.StartMinigameSelecting();
                    FocusSpot(true);
                    break;
                case LobbyState.MinigameSelected: // 미니게임 선택된 상태일 때 레디가 되면 미니게임 시작
                    if(IsHost)
                    {
                        readyComponent.ClearLobbyReady();
                        minigameComponent.StartMinigame();
                    }
                    break;
            }
        }

        // Select Minigame
        private void HandleSpaceInput()
        {
            if(IsHost == false) // Check Authority
                return;

            minigameComponent.SelectMinigame();
        }

        private void HandleMinigameSelected(int index)
        {
            if(IsHost)
                readyComponent.ClearLobbyReady();
            input.OnInteractEvent += HandleInteractInput;
            
            DisplayMinigameInfo(minigameList[index]);
        }

        private void DisplayMinigameInfo(MinigameSO minigameData)
        {
            Debug.Log($"Should Display MinigameInfo");
        }

        private void HandleInteractInput()
        {
            readyComponent.Ready();
            input.OnInteractEvent -= HandleInteractInput;
        }

        private void HandleMinigameFinished(Minigame minigame, bool cycleFinished)
        {
            FocusSpot(false);

            resultComponent.DisplayResult();
            if(cycleFinished)
                resultComponent.DisplayWinner();   
        }

        private void FocusSpot(bool focus)
        {
            InputManager.ChangeInputMap(focus ? InputMapType.UI : InputMapType.Play);
            focusVCam.Priority = focus ? DEFINE.FOCUSED_PRIORITY : DEFINE.UNFOCUSED_PRIORITY;
        }
    }
}