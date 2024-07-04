using OMG.Timers;
using UnityEngine;

namespace OMG.Minigames
{
    [RequireComponent(typeof(NetworkTimer))]
    public class TimeAttackCycle : MinigameCycle
    {
        [SerializeField] float playTime = 30f;
        private NetworkTimer timer = null;

        protected override void Awake()
        {
            base.Awake();
            timer = GetComponent<NetworkTimer>();
        }

        public void StartCycle()
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
