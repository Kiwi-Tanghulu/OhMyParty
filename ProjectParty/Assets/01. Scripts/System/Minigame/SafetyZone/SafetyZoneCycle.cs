using System;
using System.Collections;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZoneCycle : DeathmatchCycle
    {
        [SerializeField] float rerollDelay = 5f;
        [SerializeField] float rerollPostpone = 1f;
        [SerializeField] float decisionDelay = 3f;

        private SafetyZone safetyZone = null;
        private Coroutine cycle = null;

        protected override void Awake()
        {
            base.Awake();
            safetyZone = minigame as SafetyZone;
        }

        public void StartCycle(Action reroll, Action decision, Action reset)
        {
            cycle = StartCoroutine(CycleCoroutine(reroll, decision, reset));
        }

        public override void FinishCycle()
        {
            if(cycle != null)
                StopCoroutine(cycle);

            base.FinishCycle();
        }

        public override void HandlePlayerDead(ulong clientID)
        {
            if(IsHost)
            {
                safetyZone.PlayerDictionary[clientID].NetworkObject.Despawn(true);
                safetyZone.PlayerDictionary.Remove(clientID);
            }

            base.HandlePlayerDead(clientID);
        }

        private IEnumerator CycleCoroutine(Action reroll, Action decision, Action reset)
        {
            while(true)
            {
                yield return new WaitForSeconds(rerollDelay);
                reroll?.Invoke();
                // reroll
                
                yield return new WaitForSeconds(decisionDelay);
                decision?.Invoke();
                // decision

                yield return new WaitForSeconds(rerollPostpone);
                reset?.Invoke();
                // reset
            }
        }
    }
}
