using OMG.Minigames.MazeAdventure;
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

        [Space(15f)]
        [SerializeField] List<FSMParamSO> fsmParams = null;
        private Dictionary<Type, FSMParamSO> fsmParamDictionary = null;

        private bool isInit;

        public void Init()
        {
            //param
            fsmParamDictionary = new Dictionary<Type, FSMParamSO>();
            fsmParams.ForEach(i => {
                Type type = i.GetType();
                if (fsmParamDictionary.ContainsKey(type))
                    return;
                fsmParamDictionary.Add(type, ScriptableObject.Instantiate(i));
            });

            //state
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

        private void OnEnable()
        {
            currentState?.EnterState();
        }

        private void OnDisable()
        {
            currentState?.ExitState();   
        }

        public T GetFSMParam<T>() where T : FSMParamSO
        {
            return fsmParamDictionary[typeof(T)] as T;
        }

        #region Change State
        public void ChangeState(string stateName)
        {
            FSMState state = states.Find(x => x.gameObject.name == stateName);
            if (state != null)
                ChangeState(state);
        }

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

            if(IsOwner)
            {
                ChangeState(index);
            }

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
            if (IsOwner)
                return;

            ChangeState(stateIndex);
        }

        private void ChangeState(int stateIndex)
        {
            FSMState nextState = states[stateIndex];

            if (currentState == nextState)
                return;

            currentState?.ExitState();
            currentState = states[stateIndex];
            currentState.EnterState();
        }
        #endregion
    }
}
