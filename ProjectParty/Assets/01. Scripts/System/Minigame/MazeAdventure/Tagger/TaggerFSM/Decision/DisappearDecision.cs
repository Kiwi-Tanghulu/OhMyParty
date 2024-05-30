using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class DisappearDecision : FSMDecision
    {
        private DetectTargetParams targetParam = null;
        private IInvisibility invisibility = null;
        public override void Init(FSMBrain brain)
        {
            base.Init(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
        }
        public override void EnterState()
        {
            base.EnterState();
            invisibility = targetParam.Target.GetComponent<IInvisibility>();
        }

        public override bool MakeDecision()
        {
            result = invisibility.IsInvisibil;
            return base.MakeDecision();
        }

    }
}
