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
        private MoveTargetParams moveParam = null;
        private NavMeshAgent navMeshAgent;
        private DeathmatchCycle cycle = null;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
            moveParam = brain.GetFSMParam<MoveTargetParams>();
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            cycle = brain.GetComponent<Tagger>().Cycle;
        }

        protected override void OwnerUpdateState()
        {
            base.UpdateState();
            navMeshAgent.SetDestination(targetParam.Target.position);
        }

        protected override void OwnerExitState()
        {
            moveParam.movePos = targetParam.Target.position;
            base.ExitState();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MazeAdventurePlayerController player = other.GetComponent<MazeAdventurePlayerController>();
                player.PlayerDead();
                cycle.HandlePlayerDead(player.OwnerClientId);
                brain.ChangeState(nextStateIdle);
            }
        }
    }

}
