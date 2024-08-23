using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.FSM
{
    public class FSMState : MonoBehaviour
    {
        protected CharacterFSM brain;

        public UnityEvent OnStateEnterEvent;
        public UnityEvent OnStateExitEvent;

        private List<FSMTransition> transitions;
        private List<FSMAction> actions;

        public virtual void InitState(CharacterFSM brain)
        {
            this.brain = brain;

            actions = new List<FSMAction>();
            GetComponents<FSMAction>(actions);
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Init(brain);
            }

            transitions = new List<FSMTransition>();
            foreach (Transform transitionTrm in transform)
            {
                if (transitionTrm.TryGetComponent<FSMTransition>(out FSMTransition transition))
                {
                    transitions.Add(transition);
                    transition.Init(brain);
                }   
            }
        }

        public virtual void EnterState()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].EnterState();
            }

            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].EnterState();
            }

            OnStateEnterEvent?.Invoke();
        }

        public virtual void UpdateState()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].UpdateState();
            }

            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].CheckTrans())
                    break;
            }
        }

        public virtual void ExitState()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].ExitState();
            }

            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].ExitState();
            }

            OnStateExitEvent?.Invoke();
        }
    }
}
