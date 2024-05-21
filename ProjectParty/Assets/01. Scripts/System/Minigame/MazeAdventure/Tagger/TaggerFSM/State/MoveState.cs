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
        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            targetPos = targetParam.movePos;
            navMeshAgent.SetDestination(targetPos);
        }

        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();
            if (navMeshAgent.remainingDistance < 0.1f)  //FSM can play 2State lol
            {
                brain.ChangeState(nextState);
            }
        }

        public override void ExitState()
        {
            base.ExitState();

            Debug.Log(1);
        }
    }

}
