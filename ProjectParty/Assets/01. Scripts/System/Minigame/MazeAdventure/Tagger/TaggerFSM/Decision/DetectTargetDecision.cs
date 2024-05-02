using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace OMG.Minigames.MazeAdventure
{
    public class DetectTargetDecision : FSMDecision
    {
        [SerializeField] private float detectAngle;
        [SerializeField] private LayerMask groundLayer;
        private DetectTargetParams targetParam = null;

        private float checkDistance;
        public override void Init(FSMBrain brain)
        {
            base.Init(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
            checkDistance = targetParam.Radius;
        }

        public override bool MakeDecision()
        {
            RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, checkDistance, Vector3.up, 0, targetParam.TargetLayer);
            if (hitInfo.Length > 0)
            {
                foreach (RaycastHit hit in hitInfo)
                {
                    float dot = Vector3.Dot(hit.point - transform.position, transform.forward);
                    float degree = Mathf.Rad2Deg * Mathf.Acos(dot);

                    if (degree < detectAngle)
                    {
                        if(!Physics.Raycast(transform.position, (hit.point - transform.position).normalized, checkDistance * 2, groundLayer)) 
                        {
                            targetParam.Target = hit.transform;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, 40, 8);
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -40, 8);
        }
    }
}
