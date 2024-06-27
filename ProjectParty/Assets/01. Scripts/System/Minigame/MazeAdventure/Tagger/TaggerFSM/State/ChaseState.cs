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

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
            moveParam = brain.GetFSMParam<MoveTargetParams>();
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            chaseCol.enabled = true;
        }
        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("���� ���� : �߰�");
        }
        public override void UpdateState()
        {
            base.UpdateState();
            navMeshAgent.SetDestination(targetParam.Target.position);
        }

        public override void ExitState()
        {
            moveParam.movePos = targetParam.Target.position;
            base.ExitState();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MazeAdventurePlayerController player = other.GetComponent<MazeAdventurePlayerController>();
                if (player.IsInvisibil) return;
                player.PlayerDead();
                brain.ChangeState(nextStateIdle);
            }
        }
    }
}
