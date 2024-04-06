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
        private CinemachineVirtualCamera focusVCam = null;

        private void Awake()
        {
            focusVCam = transform.Find("FocusVCam").GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            readyComponent = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            readyComponent.OnLobbyReadyEvent += HandleLobbyReady;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            input.OnSpaceEvent += SelectMinigame;
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
            focusVCam.Priority = DEFINE.FOCUSED_PRIORITY;
            InputManager.ChangeInputMap(InputMapType.UI);

            Lobby.Current.ChangeLobbyState(LobbyState.MinigameSelecting);
        }

        private void SelectMinigame()
        {
            if(Lobby.Current.LobbyState != LobbyState.MinigameSelecting)
                return;

            // Set Condition

            int index = Random.Range(0, minigameList.Count);
            SetMinigameServerRpc(index);
        }

        private void DisplayMinigameInfo(MinigameSO minigameData)
        {

        }

        [ServerRpc]
        private void SetMinigameServerRpc(int index)
        {
            SetMinigameClientRpc(index);
        }

        [ClientRpc]
        private void SetMinigameClientRpc(int index)
        {
            DisplayMinigameInfo(minigameList[index]);
        }
    }
}
