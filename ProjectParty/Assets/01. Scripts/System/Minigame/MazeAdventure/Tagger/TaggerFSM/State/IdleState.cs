using OMG.FSM;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class IdleState : FSMState
    {
        [SerializeField] private FSMState testNextState;
        private float testTime;

        public override void EnterState()
        {
            base.EnterState();
            testTime = 0;
        }
        public override void UpdateState()
        {
            base.UpdateState();
            testTime += Time.deltaTime;

            if(testTime > 5f)
            {
                brain.ChangeState(testNextState);
            }
        }
    }

}
