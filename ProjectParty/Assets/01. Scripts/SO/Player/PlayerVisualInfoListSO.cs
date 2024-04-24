using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    [Serializable]
    public class PlayerVisualInfo
    {
        public PlayerVisualType VisualType;
        public Mesh mesh;
    }

    [CreateAssetMenu(menuName = "SO/Player/PlayerVisualListSO")]
    public class PlayerVisualInfoListSO : ScriptableObject
    {
        public List<PlayerVisualInfo> VisualInfoList;
    }
}