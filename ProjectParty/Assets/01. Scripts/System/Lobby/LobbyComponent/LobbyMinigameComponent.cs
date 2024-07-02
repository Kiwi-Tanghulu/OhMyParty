using System;
using System.Collections;
using System.Collections.Generic;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Minigames;
using Unity.Netcode;
using UnityEngine;
using static Unity.VisualScripting.Member;
using Random = UnityEngine.Random;

namespace OMG.Lobbies
{
    public class LobbyMinigameComponent : LobbyComponent
    {
        [SerializeField] MinigameListSO minigameList = null;

        public int MinigameCycleCount = 3;
        private int currentCycleCount = 0;
        public int CurrentCycleCount => currentCycleCount;

        /// <summary>
        /// ( MinigameData, True if Minigame Cycle Finished. False if Minigame Remaining )
        /// </summary>
        public event Action<Minigame, bool> OnMinigameFinishedEvent = null;
        public event Action OnMinigameCycleStartedEvent = null;
        public event Action OnMinigameSelectingEvent = null;
        public event Action<int> OnMinigameSelectedEvent = null;
        public event Action OnMinigameStartedEvent = null;
        private MinigameSO currentMinigame = null;

        private List<MinigameSO> notPlayedMinigameList;
        public List<MinigameSO> NotPlayedMinigameList => notPlayedMinigameList;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);

            notPlayedMinigameList = new List<MinigameSO>();
            for (int i = 0; i < minigameList.Count; i++)
            {
                notPlayedMinigameList.Add(minigameList.MinigameList[i]);
            }
        }

        public void ClearMinigameCycle()
        {
            currentCycleCount = 0;
        }

        public void StartMinigameCycle()
        {
            ClearMinigameCycle();
            BroadcastMinigameCycleStartedClientRpc();
        }

        public void StartMinigameSelecting()
        {
            if(IsHost == false)
                return;

            Lobby.ChangeLobbyState(LobbyState.MinigameSelecting);
            
            BroadcastMinigameSelectingClientRpc();
        }

        public void SelectMinigame(MinigameSO minigame)
        {
            if (IsHost == false)
                return;

            currentMinigame = minigame;
            Lobby.ChangeLobbyState(LobbyState.MinigameSelected);

            BroadcastMinigameSelectedClientRpc(minigameList.GetIndex(currentMinigame));

            currentMinigame.OnMinigameFinishedEvent += HandleMinigameFinished;

            //int index = Random.Range(0, minigameList.Count);
            //currentMinigame = minigameList[index];
            //currentMinigame.OnMinigameFinishedEvent += HandleMinigameFinished;

            ////StartCoroutine(MinigameRoulette());
            //BroadcastMinigameSelectedClientRpc(index);
        }

        public void StartMinigame()
        {
            ulong[] joinedPlayers = new ulong[Lobby.PlayerDatas.Count];
            Lobby.PlayerDatas.ForEach((i, index) => joinedPlayers[index] = i.ClientID);
            MinigameManager.Instance.StartMinigame(currentMinigame, joinedPlayers);
            BroadcastMinigameStartedClientRpc();

            notPlayedMinigameList.Remove(currentMinigame);

            Lobby.ChangeLobbyState(LobbyState.MinigamePlaying);
            Lobby.SetActive(false);
        }

        private void HandleMinigameFinished(Minigame minigame)
        {
            currentCycleCount++;
            Lobby.SetActive(true);

            bool cycleFinished = currentCycleCount >= MinigameCycleCount;
            Lobby.ChangeLobbyState(cycleFinished ? LobbyState.Community : LobbyState.MinigameFinished);
            BroadcastMinigameFinishedClientRpc(cycleFinished);
            Debug.Log($"Display Result");

            minigame.MinigameData.OnMinigameFinishedEvent -= HandleMinigameFinished;
        }

        [ClientRpc]
        private void BroadcastMinigameCycleStartedClientRpc()
        {
            OnMinigameCycleStartedEvent?.Invoke();
        }

        [ClientRpc]
        private void BroadcastMinigameStartedClientRpc()
        {
            OnMinigameStartedEvent?.Invoke();
        }

        [ClientRpc]
        private void BroadcastMinigameSelectingClientRpc()
        {
            OnMinigameSelectingEvent?.Invoke();
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
