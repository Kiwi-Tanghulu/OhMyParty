using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public abstract class FSMDecision : MonoBehaviour
    {
        protected ActioningPlayer actioningPlayer;

        [SerializeField] private bool invert;
        protected bool result;

        public virtual void Init(ActioningPlayer actioningPlayer)
        {
            this.actioningPlayer = actioningPlayer;
        }

        public abstract void EnterState();

        public virtual bool MakeDecision()
        {
            if (invert)
                result = !result;

            return result;
        }

        public abstract void ExitState();
    }
}