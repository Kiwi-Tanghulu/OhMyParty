using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private List<MinigameSO> playMinigameList;
        public List<MinigameSO> PlayMinigameList => playMinigameList;
        private List<MinigameSO> playableMinigameList;
        public List<MinigameSO> PlayableMinigameList => playableMinigameList;
        private List<MinigameSO> playedMinigameList;

        private NetworkVariable<bool> isStartCycle;
        public NetworkVariable<bool> IsStartCycle => isStartCycle;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);

            playMinigameList = new List<MinigameSO>();
            for (int i = 0; i < minigameList.Count; i++)
            {
                AddPlayAbleMinigame(minigameList.MinigameList[i]);
            }

            playedMinigameList = new List<MinigameSO>();
            MinigameCycleCount = minigameList.Count;

            isStartCycle = new NetworkVariable<bool>(false);
        }

        public void ClearMinigameCycle()
        {
            currentCycleCount = 0;
        }

        public void StartMinigameCycle()
        {
            isStartCycle.Value = true;
            isStartCycle.SetDirty(true);

            playableMinigameList = playMinigameList.ToList();

            SetMinigameCycleCountClientRpc(MinigameCycleCount);

            ClearMinigameCycle();
            BroadcastMinigameCycleStartedClientRpc();
        }

        [ClientRpc]
        private void SetMinigameCycleCountClientRpc(int count)
        {
            MinigameCycleCount = count;
        }

        public void AddPlayAbleMinigame(MinigameSO minigameSO)
        {
            if (playMinigameList.Contains(minigameSO))
                return;

            playMinigameList.Add(minigameSO);
        }

        public void RemovePlayAbleMinigame(MinigameSO minigameSO)
        {
            playMinigameList.Remove(minigameSO);
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
        }

        public void StartMinigame()
        {
            ulong[] joinedPlayers = new ulong[Lobby.PlayerDatas.Count];
            Lobby.PlayerDatas.ForEach((i, index) => joinedPlayers[index] = i.ClientID);
            MinigameManager.Instance.StartMinigame(currentMinigame, joinedPlayers);
            LightingManager.SetLightingSetting(currentMinigame.LightingSettingSO);
            BroadcastMinigameStartedClientRpc();

            playedMinigameList.Add(currentMinigame);
            playableMinigameList.Remove(currentMinigame);
            if(playableMinigameList.Count == 0)
            {
                playableMinigameList = playMinigameList.ToList();
            }

            Lobby.ChangeLobbyState(LobbyState.MinigamePlaying);
            Lobby.SetActive(false);
        }

        private void HandleMinigameFinished(Minigame minigame)
        {
            Lobby.SetActive(true);

            currentCycleCount++;
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
            Lobby.Current.ApplyLobbyLighting();

            if(!IsHost)
                currentCycleCount++;
        }
    }
}
