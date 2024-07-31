using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public abstract class Gimmick : MonoBehaviour
    {
        public UnityEvent OnExecuteEvent;

        protected virtual void Execute()
        {
            OnExecuteEvent?.Invoke();
        }

        protected abstract bool IsExecutable();
    }
}
