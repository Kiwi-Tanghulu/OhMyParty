using OMG.Extensions;
using OMG.Inputs;
using OMG.Player;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZonePlayerController : PlayerController
    {
        [SerializeField] PlayInputSO input = null;

        public bool IsSafety = false;
        public bool IsDead = false;

        public PlayerHealth Health = null;

        [SerializeField] private StarEffect starEffect;
        [SerializeField] private ParticleSystem slowEffect;
        [SerializeField] private ParticleSystem hitParticle;
        protected override void Awake()
        {
            base.Awake();

            Health = GetComponent<PlayerHealth>();
        }

        private Coroutine slowCoroutine = null;
        public void Slow(float duration, float slow)
        {
            if(slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
                GetCompo<CharacterStat>().StatSO[CharacterStatType.MaxMoveSpeed].RemoveModifier(slow);
            }

            // 슬로우 시작
            SlowEffectServerRPC(true);
            GetCompo<CharacterStat>().StatSO[CharacterStatType.MaxMoveSpeed].AddModifier(slow);
            slowCoroutine = StartCoroutine(this.DelayCoroutine(duration, () => {
                GetCompo<CharacterStat>().StatSO[CharacterStatType.MaxMoveSpeed].RemoveModifier(slow);
                // 슬로우 종료
                SlowEffectServerRPC(false);
            }));
        }

        private Coroutine inversionCoroutine = null;
        public void InvertInput(float duration)
        {
            // 입력반전 시작
            input.MoveInputInversion = true;

            DizzyStarEffectServerRPC(true);

            if(inversionCoroutine != null)
                StopCoroutine(inversionCoroutine);
            inversionCoroutine = StartCoroutine(this.DelayCoroutine(duration, () => {
                input.MoveInputInversion = true;

                // 입력반전 종료
                DizzyStarEffectServerRPC(false);
            }));
        }


        [ServerRpc]
        public void FruitItemHitEffectServerRPC()
        {
            FruitItemHitEffectClientRPC();
        }
        [ClientRpc]
        private void FruitItemHitEffectClientRPC()
        {
            ParticleSystem particle = Instantiate(hitParticle, transform.position + Vector3.up * 2, Quaternion.identity);
            particle.Play();
        }

        [ServerRpc]
        private void DizzyStarEffectServerRPC(bool value)
        {
            DizzyStarEffectClientRPC(value);
        }

        [ClientRpc]
        private void DizzyStarEffectClientRPC(bool value)
        {
            if (value) starEffect.StartEffect();
            else starEffect.StopEffect();
        }

        [ServerRpc]
        private void SlowEffectServerRPC(bool value)
        {
            SlowEffectClientRPC(value);
        }

        [ClientRpc]
        private void SlowEffectClientRPC(bool value)
        {
            if (value) slowEffect.Play();
            else slowEffect.Stop();
        }
    }
}
