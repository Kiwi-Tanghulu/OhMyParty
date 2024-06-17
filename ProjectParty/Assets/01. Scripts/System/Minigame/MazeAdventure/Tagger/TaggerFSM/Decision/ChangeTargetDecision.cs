using OMG.FSM;
using OMG.Minigames.MazeAdventure;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class ChangeTargetDecision : FSMDecision
    {
        [SerializeField] private float shortRadius;
        private DetectTargetParams targetParam = null;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);
            targetParam = brain.GetFSMParam<DetectTargetParams>();
        }
        public override bool MakeDecision()
        {
            result = false;
            RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, shortRadius, Vector3.up, 0, targetParam.TargetLayer);
            if (hitInfo.Length > 0)
            {
                targetParam.Target = hitInfo[0].transform;
                foreach (RaycastHit hit in hitInfo)
                {
                    if (Vector3.Distance(targetParam.Target.position, transform.position) < Vector3.Distance(hit.transform.position, transform.position))
                    {
                        targetParam.Target = hit.transform;
                    }
                }
                result = true;
            }
            return base.MakeDecision();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, shortRadius);
        }
#endif
    }
}
