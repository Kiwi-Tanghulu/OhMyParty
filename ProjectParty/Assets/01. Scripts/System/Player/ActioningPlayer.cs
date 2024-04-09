using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class ActioningPlayer : NetworkBehaviour
    {
        private Player player;

        private List<FSMState> states;
        [SerializeField] private FSMState startState;
        [SerializeField] private FSMState currentState;

        public override void OnNetworkSpawn()
        {
            player = PlayerManager.Instance.PlayerDic[OwnerClientId];
            player.SetActioningPlayer(this);
            transform.SetParent(player.transform);

            states = new List<FSMState>();
            Transform stateContainer = transform.Find("States");
            foreach (Transform stateTrm in stateContainer)
            {
                if (stateTrm.TryGetComponent<FSMState>(out FSMState state))
                {
                    states.Add(state);
                    state.InitState(this);
                }
            }

            if (startState == null)
            {
                Debug.Log("시작 스테이트 설정 안 됨");
            }
            else
            {
                if(IsOwner)
                    ChangeState(startState);
            }
        }

        public virtual void UpdateActioningPlayer()
        {
            currentState?.UpdateState();
        }

        public void ChangeState(FSMState state)
        {
            if (!states.Find(x => x == state))
            {
                Debug.Log($"not exist : {state}");
                return;
            }

            if (currentState == state)
                return;

            int index = states.IndexOf(state);

            ChangeStateServerRpc(index);
        }

        [ServerRpc(RequireOwnership = false)]
        private void ChangeStateServerRpc(int stateIndex)
        {
            ChangeStateClientRpc(stateIndex);
        }

        [ClientRpc]
        private void ChangeStateClientRpc(int stateIndex)
        {
            currentState?.ExitState();
            currentState = states[stateIndex];
            currentState.EnterState();
        }
    }
}
