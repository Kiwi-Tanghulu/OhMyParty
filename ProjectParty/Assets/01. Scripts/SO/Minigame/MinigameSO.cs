using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace OMG.Minigames
{
    [Serializable]
    public class ControlKeyInfo
    {
        public List<ControlKey> ControlKeys;
        public string ControlKeyName;
    }

    [CreateAssetMenu(menuName = "SO/Minigame/MinigameData")]
    public class MinigameSO : ScriptableObject
    {
        public Minigame MinigamePrefab;
        public MinigameType Type;
        
        [Space(15f)]
        public float IntroPostponeTime = 1f;
        public float OutroPostponeTime = 1f;
        public float ResultPostponeTime = 1f;

        [Space(15f)]
        public Sprite MinigameImage;
        public string MinigameName;
        [TextArea] public string MinigameDescription;
        public VideoClip Video;
        public List<ControlKeyInfo> ControlKeyInfoList;

        public Action<Minigame> OnMinigameFinishedEvent;
    }
}
