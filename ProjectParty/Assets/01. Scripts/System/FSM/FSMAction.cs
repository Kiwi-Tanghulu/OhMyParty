using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public class FSMAction : MonoBehaviour
    {
        protected CharacterFSM brain;

        public virtual void Init(CharacterFSM brain)
        {
            this.brain = brain;
        }

        public virtual void NetworkInit() { }
        public virtual void EnterState() { }
        public virtual void UpdateState() { }
        public virtual void ExitState() { }
    }
}