using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class DetectTargetDecision : FSMDecision
    {
        [SerializeField] private float detectAngle;
        private DetectTargetParams targetParam = null;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
        }

        public override bool MakeDecision()
        {
            RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, targetParam.Radius, Vector3.up, 0, targetParam.TargetLayer);
            if (hitInfo.Length > 0)
            {
                foreach (RaycastHit hit in hitInfo)
                {
                    float dot = Vector3.Dot(hit.point - transform.position, transform.forward);
                    float degree = Mathf.Rad2Deg * Mathf.Acos(dot);

                    if (degree < detectAngle)
                    {
                        targetParam.Target = hit.transform;
                        return true;
                    }
                }
            }
            return false;
        }

        //private void OnDrawGizmos()
        //{
        //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, detectAngle / 2, 4);
        //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -detectAngle / 2, 4);
        //}
    }
}
