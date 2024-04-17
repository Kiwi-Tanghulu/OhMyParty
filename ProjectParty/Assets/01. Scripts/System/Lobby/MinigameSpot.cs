using System;
using System.Collections.Generic;
using Cinemachine;
using OMG.Input;
using OMG.Interacting;
using OMG.Minigames;
using OMG.Players;
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
        private LobbyCutSceneComponent cutSceneComponent = null;

        private NetworkList<ulong> playerIdList;

        private ulong currentClientID = 0;

        private void Awake()
        {
            focusVCam = transform.Find("FocusVCam").GetComponent<CinemachineVirtualCamera>();
            playerIdList = new NetworkList<ulong>(new ulong[4]);
        }

        private void Start()
        {
            minigameComponent = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            resultComponent = Lobby.Current.GetLobbyComponent<LobbyResultComponent>();
            readyComponent = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            cutSceneComponent = Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>();

            minigameComponent.OnMinigameSelectedEvent += HandleMinigameSelected;
            minigameComponent.OnMinigameFinishedEvent += HandleMinigameFinished;
            readyComponent.OnLobbyReadyEvent += HandleLobbyReady;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }

        public bool Interact(Component performer, bool actived, Vector3 point = default)
        {
            if(actived == false)
                return false;

            NetworkObject player = performer.GetComponent<NetworkObject>();
            if(player == null)
                return false;

            if (performer.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                currentClientID = player.OwnerClientId;
                readyComponent.Ready(currentClientID);
                playerController.ChangeState(typeof(SitState));
            }

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

                    input.OnSpaceEvent += HandleSpaceInput;
                    FocusSpot(true);
                    break;
                case LobbyState.MinigameSelected: // 미니게임 선택된 상태일 때 레디가 되면 미니게임 시작
                    // 여기다가 컷씬 시작해주고
                    if(IsHost)
                        cutSceneComponent.PlayCutscene(true);
                    break;
            }
        }

        public void StartMinigame() //이거 호출하면 미니게임 시작
        {
            if (IsHost)
            {
                readyComponent.ClearLobbyReady();
                minigameComponent.StartMinigame();
            }
        }

        // Select Minigame
        private void HandleSpaceInput()
        {
            if(IsHost == false) // Check Authority Later
                return;

            minigameComponent.SelectMinigame();
            input.OnSpaceEvent -= HandleSpaceInput;
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
            readyComponent.Ready(currentClientID);
            input.OnInteractEvent -= HandleInteractInput;
        }

        private void HandleMinigameFinished(Minigame minigame, bool cycleFinished)
        {
            FocusSpot(false);

            resultComponent.DisplayResult();
            cutSceneComponent.PlayCutscene(false);
            
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
