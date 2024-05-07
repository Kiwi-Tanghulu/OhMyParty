using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace OMG.Minigames.MazeAdventure
{
    public class FindState : FSMState
    {
        [SerializeField] private FSMState nextState;
        [SerializeField] private float reconnaissanceTime;
        [SerializeField] private float reconnaissanceAngle;
        private Transform taggerTrm;
        private NavMeshAgent navMeshAgent;
        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            taggerTrm = brain.transform;
            navMeshAgent = brain.GetComponent<NavMeshAgent>();
        }

        protected override void OwnerEnterState()
        {
            base.EnterState();
            navMeshAgent.ResetPath();
            navMeshAgent.enabled = false;
            StartCoroutine(Reconnaissance());
        }

        protected override void OwnerExitState()
        {
            base.ExitState();
            StopAllCoroutines();
            navMeshAgent.enabled = true;
        }

        private IEnumerator Reconnaissance()
        {
            float timer = 0;
            float rotationY = taggerTrm.eulerAngles.y;
            while (timer < reconnaissanceTime)
            {
                timer += Time.deltaTime;
                float angle = Mathf.Lerp(rotationY, rotationY + reconnaissanceAngle, EaseOutCirc(timer / reconnaissanceTime));
                taggerTrm.rotation = Quaternion.Euler(0,angle,0);
                yield return null;
            }
            timer = 0;
            rotationY = taggerTrm.eulerAngles.y;
            while (timer < reconnaissanceTime * 2)
            {
                timer += Time.deltaTime;
                float angle = Mathf.Lerp(rotationY, rotationY - reconnaissanceAngle * 2, EaseOutCirc(timer / (reconnaissanceTime * 2)));
                taggerTrm.rotation = Quaternion.Euler(0, angle, 0);
                yield return null;
            }
            timer = 0;
            rotationY = taggerTrm.eulerAngles.y;
            while (timer < reconnaissanceTime)
            {
                timer += Time.deltaTime;
                float angle = Mathf.Lerp(rotationY, rotationY + reconnaissanceAngle, EaseOutCirc(timer / reconnaissanceTime));
                taggerTrm.rotation = Quaternion.Euler(0, angle, 0);
                yield return null;
            }
            brain.ChangeState(nextState);
        }

        private float EaseOutCirc(float value)
        {
            if (value >= 1f) value = 1f;
            return Mathf.Sqrt(1 - Mathf.Pow(value - 1, 2));
        }
    }
}
