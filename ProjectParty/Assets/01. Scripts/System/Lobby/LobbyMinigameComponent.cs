using System;
using OMG.Minigames;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OMG.Lobbies
{
    public class LobbyMinigameComponent : LobbyComponent
    {
        [SerializeField] MinigameListSO minigameList = null;

        public event Action<int> OnMinigameSelectedEvent = null;
        private MinigameSO currentMinigame = null;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);
        }

        private void Start()
        {
            MinigameManager.Instance.OnMinigameFinishEvent += HandleMinigameFinish;
        }

        public void StartMinigameSelecting()
        {
            if(IsHost == false)
                return;

            Lobby.ChangeLobbyState(LobbyState.MinigameSelecting);
        }

        public void SelectMinigame()
        {
            if (IsHost == false)
                return;

            Lobby.ChangeLobbyState(LobbyState.MinigameSelected);

            int index = Random.Range(0, minigameList.Count);
            currentMinigame = minigameList[index];

            BroadcastMinigameSelectedClientRpc(index);
        }

        public void StartMinigame()
        {
            MinigameManager.Instance.StartMinigame(currentMinigame);
            Lobby.SetActive(false);
        }

        private void HandleMinigameFinish()
        {
            Lobby.SetActive(true);
        }

        [ClientRpc]
        private void BroadcastMinigameSelectedClientRpc(int index)
        {
            OnMinigameSelectedEvent?.Invoke(index);
        }
    }
}
