using OMG.FSM;
using OMG.Inputs;
using UnityEngine;

namespace OMG.Player
{
    public class GetKeyDownLeftClick : FSMDecision
    {
        [SerializeField] private PlayInputSO input;

        public override void EnterState()
        {
            base.EnterState();

            input.OnActionEvent += SetResult;
        }

        public override void ExitState()
        {
            base.ExitState();

            input.OnActionEvent -= SetResult;
        }

        private void SetResult()
        {
            result = true;
        }
    }
}