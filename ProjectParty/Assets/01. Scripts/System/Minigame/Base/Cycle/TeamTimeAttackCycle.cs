using OMG.Timers;
using UnityEngine;

namespace OMG.Minigames
{
    [RequireComponent(typeof(NetworkTimer))]
    public class TeamTimeAttackCycle : TeamMinigameCycle
    {
        [SerializeField] float playTime = 30f;

        private NetworkTimer timer = null;

        public override void Init(Minigame minigame)
        {
            base.Init(minigame);

            timer = GetComponent<NetworkTimer>();
        }

        public virtual void StartCycle()
        {
            timer.SetTimer(playTime);
            timer.OnTimerFinishedEvent.AddListener(FinishCycle);
        }

        protected virtual void FinishCycle()
        {
            if(IsHost == false)
                return;

            ApplyScore();
            minigame.FinishGame();
        }
    }
}
