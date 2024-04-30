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

        private MoveTargetParams targetParam = null;
        private NavMeshAgent navMeshAgent;
        private Vector3 targetPos;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            targetParam = brain.GetFSMParam<MoveTargetParams>();
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
        }
        public override void EnterState()
        {
            base.EnterState();
            targetPos = targetParam.movePos;
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
