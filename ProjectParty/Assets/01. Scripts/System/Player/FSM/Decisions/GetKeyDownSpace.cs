using OMG.FSM;
using OMG.Inputs;
using UnityEngine;

namespace OMG.Player
{
    public class GetKeyDownSpace : FSMDecision
    {
        [SerializeField] private PlayInputSO input;

        public override void EnterState()
        {
            base.EnterState();

            input.OnJumpEvent += SetResult;
        }

        public override void ExitState()
        {
            base.ExitState();

            input.OnJumpEvent -= SetResult;
        }

        private void SetResult(bool started)
        {
            result = started;
        }
    }
}