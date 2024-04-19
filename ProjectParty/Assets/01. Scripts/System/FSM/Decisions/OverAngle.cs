using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.FSM
{
    public class OverAngle : FSMDecision
    {
        [SerializeField] private bool X;
        [SerializeField] private bool Y;
        [SerializeField] private bool Z;
        [SerializeField] private float maxAngle;
        [SerializeField] private float minAngle;

        public override bool MakeDecision()
        {
            Vector3 eulerAngles = brain.transform.eulerAngles;

            if(X)
                result = CheckAngle(eulerAngles.x);
            else if(Y)
                result = CheckAngle(eulerAngles.y);
            else if(Z)
                result = CheckAngle(eulerAngles.z);

            return base.MakeDecision();
        }

        private bool CheckAngle(float angle)
        {
            if (angle > 180f)
                angle -= 360f;

            return angle >= maxAngle || angle <= minAngle;
        }
    }
}