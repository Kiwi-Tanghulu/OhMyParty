using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public abstract class FSMState : MonoBehaviour
    {
        protected FSMBrain brain;

        private List<FSMTransition> transitions;
        private List<FSMAction> actions;

        public virtual void InitState(FSMBrain brain)
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

        //all
        public virtual void EnterState()
        {
            if (brain.IsOwner)
                OwnerEnterState();
        }
        //single
        protected virtual void OwnerEnterState() 
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].EnterState();
            }

            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].EnterState();
            }
        }

        //all
        public virtual void UpdateState()
        {
            if (brain.IsOwner)
            {
                OwnerUpdateState();
            }
        }
        //single
        protected virtual void OwnerUpdateState() 
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].CheckTrans();
            }

            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].UpdateState();
            }
        }

        //all
        public virtual void ExitState()
        {
            if (brain.IsOwner)
                OwnerExitState();
        }
        //single
        protected virtual void OwnerExitState() 
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].ExitState();
            }

            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].ExitState();
            }
        }
    }
}