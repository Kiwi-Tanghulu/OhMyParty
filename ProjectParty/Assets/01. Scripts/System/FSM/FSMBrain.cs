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
            //if (!IsOwner)
            //    return;

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
            isInit = true;

            if (defaultState == null)
            {
                Debug.LogError("not set start state");
            }
            else
            {
                ChangeState(defaultState);
            }
        }

        public void UpdateFSM()
        {
            if (!isInit || (!IsOwner && IsServer))
                return;
                
            currentState?.UpdateState();
        }

        private void OnEnable()
        {
            if (!isInit || (!IsOwner && IsServer))
                return;

            currentState?.EnterState();
        }

        private void OnDisable()
        {
            if (!isInit || (!IsOwner && IsServer))
                return;

            currentState?.ExitState();   
        }

        public T GetFSMParam<T>() where T : FSMParamSO
        {
            return fsmParamDictionary[typeof(T)] as T;
        }

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
                Debug.LogError($"not exist : {state}");
                return;
            }

            int index = states.IndexOf(state);

            ChangeState(index);
        }

        private void ChangeState(int stateIndex)
        {
            if (!isInit || (!IsOwner && IsServer))
                return;

            if (states == null)
                return;
            if (stateIndex >= states.Count)
                return;

            FSMState nextState = states[stateIndex];

            if (currentState == nextState)
                return;

            currentState?.ExitState();
            currentState = states[stateIndex];
            currentState.EnterState();
        }
    }
}
