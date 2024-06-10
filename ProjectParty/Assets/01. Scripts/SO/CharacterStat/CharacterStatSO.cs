using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG
{
    [CreateAssetMenu(menuName = "SO/Character/CharacterStatSO")]
    public class CharacterStatSO : ScriptableObject
    {
        [SerializeField] private List<Stat> statList;

        private Dictionary<CharacterStatType, Stat> statDic;

        public Stat this[CharacterStatType type]
        {
            get { return statDic[type]; }
        }

        public void Init()
        {
            statList.ForEach(i => i.Init());
        }

        private void OnEnable()
        {
            if(statDic == null)
                statDic = new Dictionary<CharacterStatType, Stat>();
            statDic.Clear();

            foreach(Stat stat in statList)
            {
                statDic[stat.Type] = stat;
            }
        }
    }
}