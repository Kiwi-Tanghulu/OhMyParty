using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.FSM
{
    public class FSMBrain : NetworkBehaviour
    {
        private List<FSMState> states;

        [SerializeField] private FSMState defaultState;
        public FSMState DefaultState => defaultState;
        [SerializeField] private FSMState currentState;

        private bool isInit;

        public void Init()
        {
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

            if (defaultState == null)
            {
                Debug.Log("not set start state");
            }
            else
            {
                if (IsOwner)
                    ChangeState(defaultState);
            }

            isInit = true;
        }

        public void UpdateFSM()
        {
            if (!isInit)
                return;
                
            currentState?.UpdateState();
        }

        #region Change State
        public void ChangeState(Type type)
        {
            FSMState state = states.Find(x => x.GetType() == type);
            if (state != null)
                ChangeState(state);
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
        #endregion
    }
}