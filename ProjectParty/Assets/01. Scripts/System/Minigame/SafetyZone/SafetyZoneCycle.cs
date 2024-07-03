using System;
using System.Collections;
using OMG.Timers;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZoneCycle : DeathmatchCycle
    {
        [SerializeField] float rerollDelay = 5f;
        [SerializeField] float decisionDelay = 3f;
        [SerializeField] float resetDelay = 5f;

        private Timer timer = null;
        private Coroutine cycle = null;
        private SafetyZone safetyZone = null;

        protected override void Awake()
        {
            base.Awake();
            safetyZone = minigame as SafetyZone;
            timer = GetComponent<Timer>();
        }

        public void StartCycle(Action reroll, Action decision, Action reset)
        {
            cycle = StartCoroutine(CycleCoroutine(reroll, decision, reset));
        }

        public override void FinishCycle()
        {
            if(cycle != null)
                StopCoroutine(cycle);
            timer.ResetTimer();

            base.FinishCycle();
        }

        public override void SetPlayerDead(ulong clientID)
        {
            safetyZone.PlayerDictionary.Remove(clientID);
            base.SetPlayerDead(clientID);
        }

        private IEnumerator CycleCoroutine(Action reroll, Action decision, Action reset)
        {
            reset?.Invoke();

            while(true)
            {
                // reroll
                timer.SetTimer(rerollDelay, reroll);
                yield return new WaitUntil(() => timer.Finished);
                
                // decision
                timer.SetTimer(decisionDelay, decision);
                yield return new WaitUntil(() => timer.Finished);

                // reset
                timer.SetTimer(resetDelay, reset);
                yield return new WaitUntil(() => timer.Finished);
            }
        }
    }
}
