using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public class CutsceneSignalHandler : MonoBehaviour
    {
        [SerializeField] List<UnityEvent> signalHandles = new List<UnityEvent>();
        
        private int counter  = 0;

        public void HandleSignal()
        {
            signalHandles[counter]?.Invoke();
            counter++;
        }
    }
}
