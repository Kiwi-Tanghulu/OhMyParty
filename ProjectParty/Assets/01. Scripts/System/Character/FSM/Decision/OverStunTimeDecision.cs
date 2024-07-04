using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public class OverStunTimeDecision : OverTime
    {
        private CharacterStatSO characterStatSO;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            characterStatSO = brain.GetComponent<CharacterStat>().StatSO;
        }

        public override void EnterState()
        {
            base.EnterState();

            time = characterStatSO[CharacterStatType.StunTime].Value;
        }
    }
}
