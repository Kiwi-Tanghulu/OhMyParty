using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class CharacterStat : MonoBehaviour
    {
        [SerializeField] private CharacterStatSO statSORef;
        private CharacterStatSO statSO;
        public CharacterStatSO StatSO => statSO;

        private void Awake()
        {
            statSO = ScriptableObject.Instantiate(statSORef);
        }

        public void AddModifier(CharacterStatType type, float value, float time)
        {
            StartCoroutine(AddModifierCo(type, value, time));
        }

        private IEnumerator AddModifierCo(CharacterStatType type, float value, float time)
        {
            statSO[type].AddModifier(value);

            yield return new WaitForSeconds(time);

            statSO[type].RemoveModifier(value);
        }
    }
}