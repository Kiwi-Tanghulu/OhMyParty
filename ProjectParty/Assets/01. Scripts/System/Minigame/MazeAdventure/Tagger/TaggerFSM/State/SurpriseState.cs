using OMG.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace OMG.Minigames.MazeAdventure
{
    public class SurpriseState : FSMState
    {
        [SerializeField] private TaggerTextEffect taggerTextEffect;
        private NavMeshAgent navMeshAgent;
        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            taggerTextEffect = brain.GetComponent<TaggerTextEffect>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            navMeshAgent.enabled = false;
        }

        public override void EnterState()
        {
            base.EnterState();
            taggerTextEffect.MakeTextEffect('!');
        }
        protected override void OwnerExitState()
        {
            navMeshAgent.enabled = true;
            base.OwnerExitState();
        }
    }
}
