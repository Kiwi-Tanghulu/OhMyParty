using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class AnimationTriggerAction : FSMAction
    {
        [SerializeField] private FSMState nextState;
        private AnimationTrigger animationTrigger;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);
            animationTrigger = brain.GetComponent<AnimationTrigger>();
        }

        public override void EnterState()
        {
            base.EnterState();
            animationTrigger.AnimationEnd += ChangeNextState;
        }

        private void ChangeNextState()
        {
            brain.ChangeState(nextState);
        }

        public override void ExitState()
        {
            base.ExitState();
            animationTrigger.AnimationEnd -= ChangeNextState;

        }
    }
}
