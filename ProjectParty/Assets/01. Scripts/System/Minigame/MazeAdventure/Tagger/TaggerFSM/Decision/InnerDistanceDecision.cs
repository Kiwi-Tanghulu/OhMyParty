using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class InnerDistanceDecision : FSMDecision
    {
        private DetectTargetParams targetParam = null;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
        }
        public override bool MakeDecision()
        {
            return Vector3.Distance(transform.position, targetParam.Target.position) < targetParam.Radius;  
        }
    }
}
