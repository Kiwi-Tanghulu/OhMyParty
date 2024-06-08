using OMG.Extensions;
using OMG.Inputs;
using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZonePlayerController : PlayerController
    {
        [SerializeField] PlayInputSO input = null;

        public bool IsSafety = false;
        public bool IsDead = false;

        public PlayerHealth Health = null;

        private void Awake()
        {
            Health = GetComponent<PlayerHealth>();
        }

        private Coroutine slowCoroutine = null;
        public void Slow(float duration, float slow)
        {
            if(slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
                Stat.StatSO[CharacterStatType.MaxMoveSpeed].RemoveModifier(slow);
            }

            Stat.StatSO[CharacterStatType.MaxMoveSpeed].AddModifier(slow);
            slowCoroutine = StartCoroutine(this.DelayCoroutine(duration, () => {
                Stat.StatSO[CharacterStatType.MaxMoveSpeed].RemoveModifier(slow);
            }));
        }

        private Coroutine inversionCoroutine = null;
        public void InvertInput(float duration)
        {
            input.MoveInputInversion = true;

            if(inversionCoroutine != null)
                StopCoroutine(inversionCoroutine);
            inversionCoroutine = StartCoroutine(this.DelayCoroutine(duration, () => input.MoveInputInversion = true));
        }
    }
}
