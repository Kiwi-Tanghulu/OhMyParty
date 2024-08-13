
using System;
using System.Collections.Generic;
using OMG.Network;
using OMG.NetworkEvents;
using Unity.Netcode;

namespace OMG.Minigames
{
    public class MinigameState : NetworkBehaviour
    {
        public enum StateType
        {
            None,
            Spawned,
            Initialized,
            CutsceneFinished,
            Playing,
            Finished,
            Released
        }

        public NetworkEvent<StateParams> OnStateChangedEvent = new NetworkEvent<StateParams>("StateChanged");
        public Action<StateType> OnStateSyncedEvent = null;
        public StateType State { get; private set; } = StateType.None;

        private Dictionary<ulong, StateType> minigameStates = new Dictionary<ulong, StateType>();
        private int stateSyncedPlayerCount = 0;

        public void Init()
        {
            OnStateChangedEvent.AddListener(HandleStateChanged);
            OnStateChangedEvent.Register(NetworkObject);
        }

        public void Init(ulong[] playerIDs)
        {
            foreach(ulong id in playerIDs)
                minigameStates.Add(id, StateType.None);
            
            HostManager.Instance.OnClientDisconnectedEvent += HandleClientDisconnect;
        }

        public void Release()
        {
            OnStateChangedEvent.Unregister();

            if(IsHost)
                HostManager.Instance.OnClientDisconnectedEvent -= HandleClientDisconnect;
        }

        public void ChangeMinigameState(StateType state)
        {
            if(state == State)
                return;

            State = state;

            if(IsHost)
                stateSyncedPlayerCount = 0;

            StateParams stateParams = new StateParams((int)state, ClientManager.Instance.ClientID);
            OnStateChangedEvent?.Broadcast(stateParams, false);
        }

        private void HandleStateChanged(StateParams stateParams)
        {
            StateType state = (StateType)stateParams.State;
            ulong clientID = stateParams.ClientID;

            if(IsHost) 
            {
                if(State == state) // 호스트와 같은 스테이트로 업데이트 했다면
                {
                    stateSyncedPlayerCount++;
                }
                else
                {
                    if(State == minigameStates[clientID]) // 호스트와 같은 스테이트였다가 다른 스테이트로 업데이트 했다면
                    {
                        stateSyncedPlayerCount--;
                    }
                }

                minigameStates[clientID] = state;
                if(stateSyncedPlayerCount >= minigameStates.Count)
                    OnStateSyncedEvent?.Invoke(State);
            }
        }

        private void HandleClientDisconnect(ulong clientID)
        {
            if(minigameStates[clientID] == State) // 접속 해제한 클라이언트가 호스트와 같은 스테이트였다면
                stateSyncedPlayerCount--;

            minigameStates.Remove(clientID);
            if(stateSyncedPlayerCount >= minigameStates.Count)
                OnStateSyncedEvent?.Invoke(State);
        }
    }
}