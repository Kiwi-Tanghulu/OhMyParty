using Steamworks;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.FSM
{
    public class CharacterFSM : CharacterComponent
    {
        private List<FSMState> states;

        [SerializeField] private FSMState defaultState;
        public FSMState DefaultState => defaultState;
        [SerializeField] private FSMState currentState;

        [Space(15f)]
        [SerializeField] List<FSMParamSO> fsmParams = null;
        private Dictionary<Type, FSMParamSO> fsmParamDictionary = null;

        private bool isInit;

        public override void Init(OMG.CharacterController controller)
        {
            base.Init(controller);
            
            //param
            fsmParamDictionary = new Dictionary<Type, FSMParamSO>();
            fsmParams.ForEach(i => {
                Type type = i.GetType();
                if (fsmParamDictionary.ContainsKey(type))
                    return;
                fsmParamDictionary.Add(type, ScriptableObject.Instantiate(i));
            });

            
        }

        public override void PostInitializeComponent()
        {
            base.PostInitializeComponent();

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

            #region !use in network
#if UNITY_EDITOR
            if (!Controller.UseInNetwork)
            {
                if (defaultState == null)
                {
                    Debug.LogError("not set start state");
                }
                else
                {
                    ChangeState(defaultState);
                }

                return;
            }
#endif
            #endregion

            if (defaultState == null)
            {
                Debug.LogError("not set start state");
            }
            else
            {
                if (Controller.IsOwner)
                    ChangeState(defaultState);
            }
        }

        public override void UpdateCompo()
        {
            base.UpdateCompo();

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

        public void ChangeDefaultState()
        {
            ChangeState(defaultState);
        }

        private void ChangeState(int stateIndex)
        {
            FSMState nextState = null;

            #region !use in network
#if UNITY_EDITOR
            if (!Controller.UseInNetwork)
            {
                if (states == null)
                    return;
                if (stateIndex >= states.Count)
                    return;

                nextState = states[stateIndex];

                currentState?.ExitState();
                currentState = states[stateIndex];
                currentState.EnterState();

                return;
            }
#endif
            #endregion

            if (!Controller.IsOwner)
            {
                Debug.LogError("only can change state in owner");
                return;
            }
            if (states == null)
                return;
            if (stateIndex >= states.Count)
                return;

            nextState = states[stateIndex];

            if (nextState == currentState)
            {
                return;
            }

            currentState?.ExitState();
            currentState = states[stateIndex];
            currentState.EnterState();
        }
    }
}
