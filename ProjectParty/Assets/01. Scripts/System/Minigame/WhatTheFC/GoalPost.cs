using UnityEngine;

namespace OMG.Minigames.WhatTheFC
{
    public class GoalPost : MonoBehaviour
    {
        [SerializeField] bool teamFlag = true;
        private WhatTheFC minigame = null;

        private bool active = false;

        public void Init(Minigame minigame)
        {
            this.minigame = minigame as WhatTheFC;
            active = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(active == false)
                return;

            if(other.TryGetComponent<SoccerBall>(out SoccerBall ball) == false)
                return;

            minigame.OnGoal(!teamFlag);
        }

        public void Release()
        {
            active = false;
        }
    }
}
