using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public abstract class FSMState : MonoBehaviour
    {
        protected ActioningPlayer actioningPlayer;
        protected PlayerMovement movement;
        protected PlayerAnimation anim;

        private List<FSMTransition> transitions;

        public virtual void InitState(ActioningPlayer actioningPlayer)
        {
            this.actioningPlayer = actioningPlayer;
            movement = actioningPlayer.GetComponent<PlayerMovement>();
            anim = actioningPlayer.transform.Find("Visual").GetComponent<PlayerAnimation>();

            transitions = new List<FSMTransition>();
            foreach (Transform transitionTrm in transform)
            {
                if (transitionTrm.TryGetComponent<FSMTransition>(out FSMTransition transition))
                {
                    transitions.Add(transition);
                    transition.Init(actioningPlayer);
                }
            }
        }
        public virtual void EnterState()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].EnterState();
            }
        }

        public virtual void UpdateState()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].CheckTrans();
            }
        }

        public virtual void ExitState()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].ExitState();
            }
        }
    }
}
