using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public abstract class FSMDecision : MonoBehaviour
    {
        protected FSMBrain brain;

        [SerializeField] private bool invert;
        protected bool result;

        public virtual void Init(FSMBrain brain)
        {
            this.brain = brain;
        }

        public virtual void EnterState()
        {
            result = false;
        }

        public virtual bool MakeDecision()
        {
            if (invert)
                result = !result;

            return result;
        }

        public virtual void ExitState() { }
    }
}