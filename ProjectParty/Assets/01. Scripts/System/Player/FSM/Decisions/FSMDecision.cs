using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    public abstract class FSMDecision : MonoBehaviour
    {
        protected PlayerController actioningPlayer;

        [SerializeField] private bool invert;
        protected bool result;

        public virtual void Init(PlayerController actioningPlayer)
        {
            this.actioningPlayer = actioningPlayer;
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

        public abstract void ExitState();
    }
}