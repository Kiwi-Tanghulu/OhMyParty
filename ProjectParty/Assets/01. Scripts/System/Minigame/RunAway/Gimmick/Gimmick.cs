using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public abstract class Gimmick : MonoBehaviour
    {
        public abstract void Execute();
        protected abstract bool IsExecutable();
    }
}
