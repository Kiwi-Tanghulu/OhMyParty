using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private CharacterStatType type;
        public CharacterStatType Type => type;
        [SerializeField] private float baseValue;

        private float value;
        public float Value => value;

        private List<float> modifiers;

        public Action<float> OnValueChanged;

        public Stat()
        {
            value = baseValue;
            modifiers = new List<float>();
        }

        private void CalculateValue()
        {
            value = baseValue;

            modifiers.ForEach(i => value += i);

            OnValueChanged?.Invoke(value);
        }

        public void Init()
        {
            value = baseValue;
        }

        public void AddModifier(float modifier)
        {
            modifiers.Add(modifier);
            CalculateValue();
        }

        public void RemoveModifier(float modifier)
        {
            modifiers.Remove(modifier);
            CalculateValue();
        }

        public void SetBaseValue(float value)
        {
            baseValue = value;
            CalculateValue();
        }
    }
}