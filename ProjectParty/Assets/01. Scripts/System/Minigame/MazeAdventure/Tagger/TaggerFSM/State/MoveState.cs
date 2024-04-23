using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OMG.Minigames.MazeAdventure
{
    public class MoveState : FSMState
    {
        [SerializeField] private FSMState nextState;
        [SerializeField] private Vector3[] findPos;

        private NavMeshAgent navMeshAgent;
        private Vector3 targetPos;
        public override void EnterState()
        {
            base.EnterState();
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            int randomValue = Random.Range(0, findPos.Length);
            targetPos = findPos[randomValue];

            navMeshAgent.SetDestination(targetPos);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (navMeshAgent.remainingDistance < 0.1f)
            {
                brain.ChangeState(nextState);
            }
        }
    }

}
