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
        private DeathmatchCycle cycle = null;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            cycle = brain.GetComponent<Tagger>().Cycle;
        }

        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("현재 : ChaseStateEnter");
        }

        public override void UpdateState()
        {
            base.UpdateState();
            navMeshAgent.SetDestination(targetParam.Target.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MazeAdventurePlayerController player = other.GetComponent<MazeAdventurePlayerController>();
                player.PlayerDead();
                Debug.Log("잡았다");
                cycle.HandlePlayerDead(player.OwnerClientId);
                brain.ChangeState(nextStateIdle);
            }
        }
    }

}
