using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class InnerDistanceDecision : FSMDecision
    {
        [SerializeField] private float productValue;
        private DetectTargetParams targetParam = null;
        private float maxDistance;
        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
            maxDistance = targetParam.Radius + productValue;
        }

        public override void EnterState()
        {
            base.EnterState();
        }
        public override bool MakeDecision()
        {
            result = Vector3.Distance(transform.position, targetParam.Target.position) < maxDistance;  

            return base.MakeDecision();
        }
    }
}
