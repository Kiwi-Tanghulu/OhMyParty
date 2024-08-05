using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class CharacterStat : CharacterComponent
    {
        [SerializeField] private CharacterStatSO statSORef;
        private CharacterStatSO statSO;
        public CharacterStatSO StatSO => statSO;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);
            
            statSO = ScriptableObject.Instantiate(statSORef);
            statSO.Init();
        }

        public void AddModifier(CharacterStatType type, float value, float time)
        {
            StartCoroutine(AddModifierCo(type, value, time));
        }

        public Stat GetStat(CharacterStatType type)
        {
            return statSO[type];
        }

        private IEnumerator AddModifierCo(CharacterStatType type, float value, float time)
        {
            statSO[type].AddModifier(value);

            yield return new WaitForSeconds(time);

            statSO[type].RemoveModifier(value);
        }
    }
}