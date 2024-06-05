using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    [CreateAssetMenu(menuName = "SO/Character/CharacterStatSO")]
    public class CharacterStatSO : ScriptableObject
    {
        [Header("Movement")]
        public float MaxMoveSpeed;
        public float Accelration;
        public float GravityScale;
        public float TurnSpeed;
        public float JumpPower;

        [Space]
        [Header("Health")]
        public float MaxHealth;
        public float StunTime;

        [Space]
        [Header("Attack")]
        public float AttackDelay;
    }
}