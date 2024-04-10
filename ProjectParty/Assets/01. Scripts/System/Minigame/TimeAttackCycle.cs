using UnityEngine;

namespace OMG.Minigames
{
    public class TimeAttackCycle : MinigameCycle
    {
        private float playTime = 60f;
        private float timer = 0f;

        private bool isPlaying = false;

        private void Update()
        {
            if(isPlaying == false)
                return;

            timer += Time.deltaTime;
            if(timer >= playTime)
            {
                FinishCycle();
                OnCycleFinish();
            }
        }

        public void SetPlayTime(float playTime)
        {
            this.playTime = playTime;
        }

        public void StartCycle()
        {
            timer = 0f;
            isPlaying = true;
        }

        public void FinishCycle()
        {
            isPlaying = false;
        }

        private void OnCycleFinish()
        {
            minigame.FinishGame();
        }
    }
}
