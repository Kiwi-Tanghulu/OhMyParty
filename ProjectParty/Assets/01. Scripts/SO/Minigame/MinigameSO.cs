using System;
using UnityEngine;

namespace OMG.Minigames
{
    
    [CreateAssetMenu(menuName = "SO/Minigame/MinigameData")]
    public class MinigameSO : ScriptableObject
    {
        public Minigame MinigamePrefab;

        public Action<Minigame> OnMinigameFinishedEvent;
    }
}