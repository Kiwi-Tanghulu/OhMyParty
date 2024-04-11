using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    public abstract class FSMState : MonoBehaviour
    {
        protected PlayerController actioningPlayer;
        protected PlayerMovement movement;
        protected PlayerAnimation anim;

        private List<FSMTransition> transitions;

        public virtual void InitState(PlayerController actioningPlayer)
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

        //all
        public virtual void EnterState()
        {
            if (actioningPlayer.IsOwner)
                OwnerEnterState();
        }
        //single
        protected virtual void OwnerEnterState() 
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].EnterState();
            }
        }

        //all
        public virtual void UpdateState()
        {
            if (actioningPlayer.IsOwner)
                OwnerUpdateState();
        }
        //single
        protected virtual void OwnerUpdateState() 
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].CheckTrans();
            }
        }

        //all
        public virtual void ExitState()
        {
            if (actioningPlayer.IsOwner)
                OwnerExitState();
        }
        //single
        protected virtual void OwnerExitState() 
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].ExitState();
            }
        }
    }
}
