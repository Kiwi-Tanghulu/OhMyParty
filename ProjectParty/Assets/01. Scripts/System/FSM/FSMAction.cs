using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public class FSMAction : MonoBehaviour
    {
        protected FSMBrain brain;

        public virtual void Init(FSMBrain brain)
        {
            this.brain = brain;
        }

        public virtual void EnterState() { }
        public virtual void UpdateState() { }
        public virtual void ExitState() { }
    }
}