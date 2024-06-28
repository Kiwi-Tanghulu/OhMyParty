using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace OMG.Minigames.MazeAdventure
{
    public class ChaseExitTransition : FSMTransition
    {
        public UnityEvent OnExitStateEvent;

        public override void ExitState()
        {
            if (result)
            {
                OnExitStateEvent?.Invoke();
            }
            OnExitStateEvent?.Invoke();
            base.ExitState();
        }
    }
}
