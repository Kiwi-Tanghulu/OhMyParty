using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerController : PlayerController
    {
        [SerializeField] private FSMState dieState;
        [SerializeField] private UnityEvent dieEvent;
        public void PlayerDead()
        {
            dieEvent?.Invoke();
            StateMachine.ChangeState(dieState);
        }
    }
}
