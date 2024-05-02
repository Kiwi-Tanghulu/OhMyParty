using System;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    [Serializable]
    public class ControlKeyInfo
    {
        public List<Sprite> ControlKeyImages;
        public string ControlKeyName;
    }

    [CreateAssetMenu(menuName = "SO/Minigame/MinigameData")]
    public class MinigameSO : ScriptableObject
    {
        public Minigame MinigamePrefab;
        
        [Space(15f)]
        public float IntroPostponeTime = 1f;
        public float OutroPostponeTime = 1f;
        public float ResultPostponeTime = 1f;

        [Space(15f)]
        public string MinigameName;
        public Sprite MinigameImage;
        public List<ControlKeyInfo> ControlKeyInfoList;

        public Action<Minigame> OnMinigameFinishedEvent;
    }
}
