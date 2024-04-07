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

        private LobbyReadyComponent readyComponent = null;
        private LobbyMinigameComponent minigameComponent = null;
        private CinemachineVirtualCamera focusVCam = null;

        private void Awake()
        {
            focusVCam = transform.Find("FocusVCam").GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            minigameComponent = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            readyComponent = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();

            minigameComponent.OnMinigameSelectedEvent += HandleMinigameSelected;
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
                    if(IsHost)
                        minigameComponent.StartMinigameSelecting();
                    focusVCam.Priority = DEFINE.FOCUSED_PRIORITY;
                    InputManager.ChangeInputMap(InputMapType.UI);
                    break;
                case LobbyState.MinigameSelected: // 미니게임 선택된 상태일 때 레디가 되면 미니게임 시작
                    if(IsHost)
                        minigameComponent.StartMinigame();
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
    }
}
