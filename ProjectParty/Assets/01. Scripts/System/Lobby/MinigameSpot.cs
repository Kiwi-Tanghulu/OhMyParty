using System;
using System.Collections.Generic;
using Cinemachine;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Interacting;
using OMG.Minigames;
using OMG.Network;
using OMG.Player;
using OMG.Player.FSM;
using OMG.UI;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Lobbies
{
    public class MinigameSpot : NetworkBehaviour, IInteractable
    {
        [SerializeField] UIInputSO input = null;
        [SerializeField] MinigameListSO minigameList = null;

        [Space]
        [SerializeField] private CinemachineVirtualCamera tvFocusCam;
        
        private CinemachineVirtualCamera focusVCam = null;
        private LobbyMinigameComponent minigameComponent = null;
        private LobbyResultComponent resultComponent = null;
        private LobbyReadyComponent readyComponent = null;
        private LobbyCutSceneComponent cutSceneComponent = null;

        private NetworkList<ulong> playerIdList;

        private ulong currentClientID = 0;

        public UnityEvent OnInteractEvent;

        [Space]
        [SerializeField] private MinigameSpotUI spotUI;

        [Space]
        [SerializeField] private MinigameSpotTVUI spotTVUI;

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
            minigameComponent.OnMinigameSelectingEvent += HandleMinigameSelecting;
            minigameComponent.OnMinigameStartedEvent += HandleMinigameStarted;
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

            FocusSpot(true);

            if (performer.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                currentClientID = player.OwnerClientId;
                readyComponent.Ready(currentClientID);
                playerController.StateMachine.ChangeState(typeof(SitState));
            }

            OnInteractEvent?.Invoke();

            return true;
        }

        private void HandleLobbyReady()
        {
            switch(Lobby.Current.LobbyState)
            {
                case LobbyState.Community: // 커뮤니티 상태일 때 레디가 되면 미니게임 선택 시작
                    if(IsHost)
                    {
                        minigameComponent.StartMinigameCycle();
                        minigameComponent.StartMinigameSelecting();
                    }

                    input.OnSpaceEvent += HandleSpaceInput;
                    break;
                case LobbyState.MinigameFinished: // 그 전 미니게임이 끝난 상태일 때도 마찬가지
                    if(IsHost)
                        minigameComponent.StartMinigameSelecting();
                    input.OnSpaceEvent += HandleSpaceInput;

                    //FocusSpot(true);//move interact
                    break;
                case LobbyState.MinigameSelected: // 미니게임 선택된 상태일 때 레디가 되면 미니게임 시작
                    StartMinigame();
                    break;
            }
        }

        public void StartMinigame() //이거 호출하면 미니게임 시작
        {
            Debug.Log("start game");
            Fade.Instance.FadeOut(0f, () =>
            {
                InputManager.SetInputEnable(false);
            }, () =>
            {
                if (IsHost)
                {
                    readyComponent.ClearLobbyReady();
                    minigameComponent.StartMinigame();
                }
            });
        }

        // Select Minigame
        private void HandleSpaceInput()
        {
            if(IsHost == false) // Check Authority Later
                return;

            OnSpaceInputClientRpc();
            input.OnSpaceEvent -= HandleSpaceInput;
        }

        [ClientRpc]
        private void OnSpaceInputClientRpc()
        {
            spotUI.PickUI.StopRoulette(() =>
            {
                if(IsServer)
                {
                    minigameComponent.SelectMinigame(spotUI.PickUI.SelectedSlot.MinigameSO);
                }
            });
        }

        private void HandleMinigameSelecting()
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            Debug.Log("1");
            InputManager.SetInputEnable(false);

            CameraManager.Instance.ChangeCamera(tvFocusCam, 2f, null, () =>
            {
                if(!spotTVUI.IsInit)
                {
                    spotTVUI.Init();
                    spotTVUI.SetRoundCountValue(minigameComponent.MinigameCycleCount);
                    spotTVUI.Show();
                }
                spotTVUI.SetRoundCountText(minigameComponent.CurrentCycleCount + 1);
                spotTVUI.PlayRoundTextChangeAnim(1f, null, () =>
                {
                    StartCoroutine(this.DelayCoroutine(1f, () => spotUI.Show()));
                });
            });
        }

        private void HandleMinigameStarted()
        {
            spotUI.Hide();
        }

        private void HandleMinigameSelected(int index)
        {
            if(IsHost)
                readyComponent.ClearLobbyReady();
            input.OnInteractEvent += HandleInteractInput;
            cutSceneComponent.PlayCutscene(true);//here

            spotUI.PickUI.Hide();
            //spotUI.InfoContainer.Show();
            Debug.Log(11111);
            DisplayMinigameInfo(minigameList[index]);
        }

        private void DisplayMinigameInfo(MinigameSO minigameData)
        {
            Debug.Log($"Should Display MinigameInfo");
        }

        private void HandleInteractInput()
        {
            input.OnInteractEvent -= HandleInteractInput;
            readyComponent.Ready(currentClientID);
        }

        private void HandleMinigameFinished(Minigame minigame, bool cycleFinished)
        {
            FocusSpot(false);

            resultComponent.DisplayResult();
            cutSceneComponent.PlayCutscene(false);
            foreach(PlayerData data in Lobby.Current.PlayerDatas)
            {
                spotTVUI.SetPlayerGraphScore(data);
            }
            
            if(cycleFinished)
                resultComponent.DisplayWinner();   
        }

        public void FocusSpot(bool focus)
        {
            if (focus == false)
                InputManager.ChangeInputMap(InputMapType.Play);

            if (focus)
                CameraManager.Instance.ChangeCamera(focusVCam);
            else
                CameraManager.Instance.ChangeCamera(Lobby.Current.GetLobbyComponent<LobbySkinComponent>().Skin.Cam);
        }
    }
}
