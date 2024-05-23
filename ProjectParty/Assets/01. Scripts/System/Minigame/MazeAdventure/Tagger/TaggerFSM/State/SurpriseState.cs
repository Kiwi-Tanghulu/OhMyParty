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
        private NavMeshAgent navMeshAgent;
        private EmotionText emotionText;
        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
            emotionText = brain.transform.Find("TaggerEmotionText").GetComponent<EmotionText>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            navMeshAgent.enabled = false;
        }

        public override void EnterState()
        {
            base.EnterState();
            emotionText.StartEffect('!');
        }
        protected override void OwnerExitState()
        {
            navMeshAgent.enabled = true;
            base.OwnerExitState();
        }
    }
}
