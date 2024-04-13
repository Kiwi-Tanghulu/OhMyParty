using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    public class GetKeyDown : FSMDecision
    {
        [SerializeField] private List<KeyCode> keyCode;

        public override bool MakeDecision()
        {
            for (int i = 0; i < keyCode.Count; i++)
            {
                if (UnityEngine.Input.GetKeyDown(keyCode[i]))
                {
                    result = true;
                    break;
                }
            }

            base.MakeDecision();

            return result;
        }

        public override void ExitState()
        {
            
        }
    }
}