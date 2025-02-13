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
        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            taggerTextEffect = brain.GetComponent<TaggerTextEffect>();
        }

        public override void EnterState()
        {
            base.EnterState();

            taggerTextEffect.MakeTextEffectClientRPC('!');

            navMeshAgent.enabled = false;
        }

        public override void ExitState()
        {
            base.ExitState();

            navMeshAgent.enabled = true;
            base.ExitState();
        }
    }
}
