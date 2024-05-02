using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class AnimationTrigger : MonoBehaviour
    {
        public event Action AnimationEnd;
        private void AnimationEndTrigger()
        {
            AnimationEnd?.Invoke();
            Debug.Log("애니메이션 끝남 이벤트 발행");
        }
    }
}
