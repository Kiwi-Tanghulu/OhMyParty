using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class GetKeyDown : FSMDecision
    {
        [SerializeField] private KeyCode keyCode;

        public override void EnterState()
        {
            
        }

        public override bool MakeDecision()
        {
            if(UnityEngine.Input.GetKeyDown(keyCode))
                result = true;
            else 
                result = false;

            base.MakeDecision();

            return result;
        }

        public override void ExitState()
        {
            
        }
    }
}