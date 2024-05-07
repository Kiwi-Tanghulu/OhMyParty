using OMG.FSM;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace OMG.Minigames.MazeAdventure
{
    [System.Serializable]
    public struct StatesData
    {
        public FSMState nextState;
        public float weight;
    }
    public class IdleState : FSMState
    {
        [SerializeField] private StatesData[] nextStatesData;
        [SerializeField] private Vector3[] findPos;

        private float maxWeight = 0;
        private float randomValue;
        private float cumulativeWeight;
        private MoveTargetParams targetParam = null;
        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);
            foreach(var  data in nextStatesData) 
            { 
                maxWeight += data.weight;
            }
            targetParam = brain.GetFSMParam<MoveTargetParams>();
        }
        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();
            int randomValue = Random.Range(0, findPos.Length);
            targetParam.movePos = findPos[randomValue];
            StartCoroutine(SetNextState());
        }

        private IEnumerator SetNextState()
        {
            yield return new WaitForSeconds(1f);
            randomValue = Random.Range(0, maxWeight);
            cumulativeWeight = 0;

            foreach (var data in nextStatesData)
            {
                cumulativeWeight += data.weight;

                if (cumulativeWeight >= randomValue)
                {
                    brain.ChangeState(data.nextState);
                    break;
                }
            }
        }
    }

}
