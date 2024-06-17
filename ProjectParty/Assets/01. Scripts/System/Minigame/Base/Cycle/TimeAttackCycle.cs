using OMG.Timers;
using UnityEngine;

namespace OMG.Minigames
{
    [RequireComponent(typeof(NetworkTimer))]
    public class TimeAttackCycle : MinigameCycle
    {
        private NetworkTimer timer = null;

        protected override void Awake()
        {
            base.Awake();
            timer = GetComponent<NetworkTimer>();
        }

        public void StartCycle(float playTime)
        {
            timer.SetTimer(playTime);
            timer.OnTimerFinishedEvent.AddListener(FinishCycle);
        }

        protected void FinishCycle()
        {
            minigame.FinishGame();
        }
    }
}
