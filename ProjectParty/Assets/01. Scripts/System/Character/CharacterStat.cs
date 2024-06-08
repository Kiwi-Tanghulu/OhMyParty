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
    }
}