using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    [Serializable]
    public class ControlKeyInfo
    {
        public List<Sprite> ControlKeysImage;
        public string ControlKeyName;
    }

    [CreateAssetMenu(menuName = "SO/Minigame/MinigameInfo")]
    public class MinigameInfoSO : ScriptableObject
    {
        public Sprite MinigameImage;
        public List<ControlKeyInfo> ControlKeyInfoList;
    }
}