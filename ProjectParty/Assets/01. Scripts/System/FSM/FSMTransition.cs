using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public class FSMTransition : MonoBehaviour
    {
        private CharacterFSM brain;

        [SerializeField] private FSMState nextState;
        private FSMDecision[] decisions;
        protected bool result;

        public virtual void Init(CharacterFSM brain)
        {
            this.brain = brain;

            decisions = GetComponents<FSMDecision>();
            foreach (FSMDecision decision in decisions)
                decision.Init(brain);
        }

        public virtual void EnterState()
        {
            for (int i = 0; i < decisions.Length; i++)
            {
                decisions[i].EnterState();
            }
        }

        public bool CheckTrans()
        {
            result = false;

            for (int i = 0; i < decisions.Length; i++)
            {
                result = decisions[i].MakeDecision();

                if (!result)
                    break;
            }

            if (result)
                brain.ChangeState(nextState);

            return result;
        }

        public virtual void ExitState()
        {
            for (int i = 0; i < decisions.Length; i++)
            {
                decisions[i].ExitState();
            }
        }
    }
}
