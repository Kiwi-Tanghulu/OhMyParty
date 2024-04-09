using System;
using OMG.Extensions;
using OMG.Minigames;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OMG.Lobbies
{
    public class LobbyMinigameComponent : LobbyComponent
    {
        [SerializeField] MinigameListSO minigameList = null;

        public int MinigameCycleCount = 3;
        private int currentCycleCount = 0;

        /// <summary>
        /// ( MinigameData, True if Minigame Cycle Finished. False if Minigame Remaining )
        /// </summary>
        public event Action<Minigame, bool> OnMinigameFinishedEvent = null;
        public event Action<int> OnMinigameSelectedEvent = null;
        private MinigameSO currentMinigame = null;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);
        }

        public void ClearMinigameCycle()
        {
            currentCycleCount = 0;
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
            currentMinigame.OnMinigameFinishedEvent += HandleMinigameFinished;

            BroadcastMinigameSelectedClientRpc(index);
        }

        public void StartMinigame()
        {
            ulong[] joinedPlayers = new ulong[Lobby.PlayerDatas.Count];
            Lobby.PlayerDatas.ForEach((i, index) => joinedPlayers[index] = i.clientID);
            MinigameManager.Instance.StartMinigame(currentMinigame, joinedPlayers);

            Lobby.ChangeLobbyState(LobbyState.MinigamePlaying);
            Lobby.SetActive(false);
        }

        private void HandleMinigameFinished(Minigame minigame)
        {
            currentCycleCount++;
            BroadcastMinigameFinishedClientRpc(currentCycleCount >= MinigameCycleCount);

            Lobby.SetActive(true);
            Lobby.ChangeLobbyState(LobbyState.MinigameFinished);
            Debug.Log($"Display Result");

            minigame.MinigameData.OnMinigameFinishedEvent -= HandleMinigameFinished;
        }

        [ClientRpc]
        private void BroadcastMinigameSelectedClientRpc(int index)
        {
            OnMinigameSelectedEvent?.Invoke(index);
        }

        [ClientRpc]
        private void BroadcastMinigameFinishedClientRpc(bool cycleFinished)
        {
            OnMinigameFinishedEvent?.Invoke(MinigameManager.Instance.CurrentMinigame, cycleFinished);
        }
    }
}
