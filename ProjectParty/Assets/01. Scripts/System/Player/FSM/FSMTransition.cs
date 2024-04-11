using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    public class FSMTransition : MonoBehaviour
    {
        private PlayerController actioningPlayer;

        [SerializeField] private FSMState nextState;
        private FSMDecision[] decisions;
        private bool result;

        public void Init(PlayerController actioningPlayer)
        {
            this.actioningPlayer = actioningPlayer;

            decisions = GetComponents<FSMDecision>();
            foreach (FSMDecision decision in decisions)
                decision.Init(actioningPlayer);
        }

        public virtual void EnterState()
        {
            for (int i = 0; i < decisions.Length; i++)
            {
                decisions[i].EnterState();
            }
        }

        public void CheckTrans()
        {
            result = false;

            for (int i = 0; i < decisions.Length; i++)
            {
                result = decisions[i].MakeDecision();

                if (!result)
                    break;
            }


            if (result)
                actioningPlayer.ChangeState(nextState);
        }

        public virtual void ExitState()
        {
            for (int i = 0; i < decisions.Length; i++)
            {
                decisions[i].ExitState();
            }
        }
    }
}
