using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class OverTime : FSMDecision
    {
        [SerializeField] private float time;
        private float currentTime;

        public override void EnterState()
        {
            currentTime = 0f;
        }

        public override bool MakeDecision()
        {
            currentTime += Time.deltaTime;
            result = currentTime >= time;

            return base.MakeDecision();
        }

        public override void ExitState()
        {
            currentTime = 0f;
        }
    }
}