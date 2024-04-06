using System;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class ActioningPlayer : MonoBehaviour
    {
        protected Player player;

        private Dictionary<Type, FSMState> states;
        private FSMState currentState;
        [SerializeField] private FSMState startState;

        public virtual void InitActioningPlayer(Player player)
        {
            this.player = player;

            states = new Dictionary<Type, FSMState>();
            Transform stateContainer = transform.Find("States");
            foreach(Transform stateTrm in stateContainer)
            {
                if(stateTrm.TryGetComponent<FSMState>(out FSMState state))
                {
                    states.Add(state.GetType(), state);
                    state.InitState(this);
                }
            }

            if (startState == null)
                Debug.Log("시작 스테이트 설정 안 됨");
            else
                ChangeState(startState.GetType());
        }

        public virtual void UpdateActioningPlayer()
        {
            currentState?.UpdateState();
        }

        public void ChangeState(Type type)
        {
            if(!states.ContainsKey(type))
            {
                Debug.Log($"not exist : {type}");
                return;
            }

            currentState?.ExitState();
            currentState = states[type];
            currentState.EnterState();
        }
    }
}
