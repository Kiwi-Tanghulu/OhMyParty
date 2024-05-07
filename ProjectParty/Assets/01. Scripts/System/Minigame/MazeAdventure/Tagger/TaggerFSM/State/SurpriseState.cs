using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace OMG.Minigames.MazeAdventure
{
    public class SurpriseState : FSMState
    {
        private NavMeshAgent navMeshAgent;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            navMeshAgent.enabled = false;
        }
        protected override void OwnerExitState()
        {
            navMeshAgent.enabled = true;
            base.ExitState();
        }
    }
}
