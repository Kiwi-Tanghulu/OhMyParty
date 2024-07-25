using System.Collections.Generic;
using OMG.Extensions;
using OMG.Timers;
using UnityEngine;

namespace OMG.Minigames
{
    [RequireComponent(typeof(NetworkTimer))]
    public class TimeAttackCycle : MinigameCycle
    {
        [SerializeField] float playTime = 30f;
        [SerializeField] int[] scoreWeight = { 50, 100, 500, 1000 };
        
        private NetworkTimer timer = null;

        protected override void Awake()
        {
            base.Awake();
            timer = GetComponent<NetworkTimer>();
        }

        public virtual void StartCycle()
        {
            timer.SetTimer(playTime);
            timer.OnTimerFinishedEvent.AddListener(FinishCycle);
        }

        protected virtual void FinishCycle()
        {
            // test
            List<int> scores = new List<int>();
            minigame.PlayerDatas.ForEach(i => scores.Add(i.score));
            scores.Sort();

            minigame.PlayerDatas.ChangeAllData(data => {
                int index = scores.IndexOf(data.score);
                data.score = scoreWeight[index];
                return data;
            });
            
            minigame.FinishGame();
        }
    }
}
