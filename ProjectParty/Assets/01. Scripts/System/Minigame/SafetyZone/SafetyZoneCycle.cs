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

        private Coroutine cycle = null;

        public void StartCycle(Action reroll, Action decision)
        {
            cycle = StartCoroutine(CycleCoroutine(reroll, decision));
        }

        public override void FinishCycle()
        {
            if(cycle != null)
                StopCoroutine(cycle);

            base.FinishCycle();
        }

        private IEnumerator CycleCoroutine(Action reroll, Action decision)
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
            }
        }
    }
}
