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
        private Dictionary<PlayerVisualType, Mesh> visualInfoDic;

        public Mesh this[PlayerVisualType type]
        {
            get { return visualInfoDic[type]; }
        }

        private void OnEnable()
        {
            if(visualInfoDic == null)
            {
                visualInfoDic = new Dictionary<PlayerVisualType, Mesh>();

                for(int i = 0; i < VisualInfoList.Count; i++)
                {
                    PlayerVisualInfo info = VisualInfoList[i];

                    visualInfoDic[info.VisualType] = info.mesh;
                }
            }
        }
    }
}