using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OMG.Minigames.MazeAdventure
{
    public class ChaseState : FSMState
    {
        [SerializeField] private FSMState nextStateIdle;
        [SerializeField] private FSMState nextStateFind;
        private DetectTargetParams targetParam = null;
        private NavMeshAgent navMeshAgent;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (!(Vector3.Distance(targetParam.Target.position,transform.position) > targetParam.Radius))
            {
                navMeshAgent.SetDestination(targetParam.Target.position);
            }

            if(navMeshAgent.remainingDistance < 0.1f)
            {
                brain.ChangeState(nextStateFind);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

                brain.ChangeState(nextStateIdle);
            }
        }
    }

}
