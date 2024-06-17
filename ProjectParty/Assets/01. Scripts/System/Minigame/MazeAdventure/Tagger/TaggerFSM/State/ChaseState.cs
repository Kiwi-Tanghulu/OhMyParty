using OMG.FSM;
using System;
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
        [SerializeField] private CapsuleCollider chaseCol;
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
            chaseCol.enabled = true;
        }
        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            Debug.Log("유령 상태 : 추격");
        }
        protected override void OwnerUpdateState()
        {
            base.OwnerUpdateState();
            navMeshAgent.SetDestination(targetParam.Target.position);
        }

        protected override void OwnerExitState()
        {
            moveParam.movePos = targetParam.Target.position;
            base.OwnerExitState();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MazeAdventurePlayerController player = other.GetComponent<MazeAdventurePlayerController>();
                if (player.IsInvisibil) return;
                player.PlayerDead();
                cycle.HandlePlayerDead(player.OwnerClientId);
                brain.ChangeState(nextStateIdle);
            }
        }
    }

}
