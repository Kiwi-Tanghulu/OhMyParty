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
        [SerializeField] private LayerMask triggerLayer;
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
            result = false;
            RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, checkDistance, Vector3.up, 0, targetParam.TargetLayer);
            if (hitInfo.Length > 0)
            {
                foreach (RaycastHit hit in hitInfo)
                {
                    Vector3 targetDir = hit.transform.position - transform.position;
                    float dot = Vector3.Dot(targetDir.normalized, transform.forward);
                    float degree = Mathf.Rad2Deg * Mathf.Acos(dot);
                    if (degree < detectAngle / 2f)
                    {
                        if (!Physics.Raycast(transform.position, ((hit.transform.position + Vector3.up) - transform.position).normalized, checkDistance, triggerLayer))
                        {
                            if (!hit.transform.GetComponent<IInvisibility>().IsInvisibil)
                            {
                                targetParam.Target = hit.transform;
                                return true;
                            }
                        }
                    }
                }
            }
            return base.MakeDecision();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, 30, 9);
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -30, 9);
        }
#endif
    }
}
