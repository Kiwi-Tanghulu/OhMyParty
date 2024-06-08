using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.FSM
{
    public class OverStunTimeDecision : OverTime
    {
        [Space]
        [SerializeField] private CharacterStatSO characterStatSO;

        public override void Init(FSMBrain brain)
        {
            base.Init(brain);

            time = characterStatSO[CharacterStatType.StunTime].Value;
        }
    }
}
