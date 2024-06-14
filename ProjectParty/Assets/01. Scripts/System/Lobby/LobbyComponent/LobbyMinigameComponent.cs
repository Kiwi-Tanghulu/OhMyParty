using System;
using System.Collections;
using OMG.Extensions;
using OMG.Inputs;
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
        public event Action<int> OnMinigameRouletteChangeEvent = null;
        public event Action OnMinigameSelectingEvent = null;
        public event Action<int> OnMinigameSelectedEvent = null;
        public event Action OnMinigameStartedEvent = null;
        private MinigameSO currentMinigame = null;
        private int currentMinigameIndex;

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

            StartCoroutine(MinigameRoulette());
            
            BroadcastMinigameSelectingClientRpc();
        }

        public void SelectMinigame()
        {
            if (IsHost == false)
                return;

            StopAllCoroutines();

            Lobby.ChangeLobbyState(LobbyState.MinigameSelected);

            BroadcastMinigameSelectedClientRpc(currentMinigameIndex);

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
            Lobby.PlayerDatas.ForEach((i, index) => joinedPlayers[index] = i.clientID);
            MinigameManager.Instance.StartMinigame(currentMinigame, joinedPlayers);
            BroadcastMinigameStartedClientRpc();

            Lobby.ChangeLobbyState(LobbyState.MinigamePlaying);
            Lobby.SetActive(false);
        }

        private void HandleMinigameFinished(Minigame minigame)
        {
            currentCycleCount++;
            Lobby.SetActive(true);
            Lobby.ChangeLobbyState(LobbyState.MinigameFinished);

            BroadcastMinigameFinishedClientRpc(currentCycleCount >= MinigameCycleCount);
            Debug.Log($"Display Result");

            minigame.MinigameData.OnMinigameFinishedEvent -= HandleMinigameFinished;
        }

        private IEnumerator MinigameRoulette()
        {
            currentMinigameIndex = 0;

            while (true)
            {
                currentMinigame = minigameList[currentMinigameIndex];

                BroadcastMinigameRouletteClientRpc(currentMinigameIndex);

                currentMinigameIndex = (currentMinigameIndex + 1) % minigameList.Count;

                yield return new WaitForSeconds(0.1f);
            }
        }

        [ClientRpc]
        private void BroadcastMinigameStartedClientRpc()
        {
            OnMinigameStartedEvent?.Invoke();
        }

        [ClientRpc]
        private void BroadcastMinigameRouletteClientRpc(int index)
        {
            OnMinigameRouletteChangeEvent?.Invoke(index);
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
