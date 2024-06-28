using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public class FSMTransition : MonoBehaviour
    {
        private FSMBrain brain;

        [SerializeField] private FSMState nextState;
        private FSMDecision[] decisions;
        protected bool result;

        public virtual void Init(FSMBrain brain)
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

        public void CheckTrans()
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
