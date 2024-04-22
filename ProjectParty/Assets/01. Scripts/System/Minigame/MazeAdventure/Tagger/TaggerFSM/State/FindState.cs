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

        public override void EnterState()
        {
            base.EnterState();
            navMeshAgent.ResetPath();
            StartCoroutine(Reconnaissance());
        }

        public override void ExitState()
        {
            base.ExitState();
            StopAllCoroutines();
        }

        private IEnumerator Reconnaissance()
        {
            float timer = 0;
            float rotationY = taggerTrm.localRotation.y;
            while (timer < reconnaissanceTime / 4)
            {
                timer += Time.deltaTime;
                taggerTrm.localRotation = Quaternion.Euler(new Vector3(0, rotationY + EaseInCirc(timer) * reconnaissanceAngle, 0));
                yield return null;
            }
            timer = 0;
            rotationY = taggerTrm.localRotation.y;
            while (timer < reconnaissanceTime / 2)
            {
                timer += Time.deltaTime;
                taggerTrm.localRotation = Quaternion.Euler(new Vector3(0, rotationY + -(EaseInCirc(timer) * reconnaissanceAngle * 2), 0));
                yield return null;
            }
            timer = 0;
            rotationY = taggerTrm.localRotation.y;
            while (timer < reconnaissanceTime / 4)
            {
                timer += Time.deltaTime;
                taggerTrm.localRotation = Quaternion.Euler(new Vector3(0, rotationY + EaseInCirc(timer) * reconnaissanceAngle, 0));
                yield return null;
            }
            brain.ChangeState(nextState);
        }

        private float EaseInCirc(float value)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(value, 2));
        }
    }
}
