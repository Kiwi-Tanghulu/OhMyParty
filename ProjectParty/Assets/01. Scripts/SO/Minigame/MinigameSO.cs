using System;
using UnityEngine;

namespace OMG.Minigames
{
    
    [CreateAssetMenu(menuName = "SO/Minigame/MinigameData")]
    public class MinigameSO : ScriptableObject
    {
        public Minigame MinigamePrefab;
        
        [Space(15f)]
        public float IntroPostponeTime = 1f;
        public float OutroPostponeTime = 1f;
        public float ResultPostponeTime = 1f;

        public Action<Minigame> OnMinigameFinishedEvent;
    }
}
