using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    [CreateAssetMenu(menuName = "SO/Player/PlayerVisualSOListSO")]
    public class PlayerVisualSOListSO : ScriptableObject
    {
        [SerializeField] private List<PlayerVisualSO> visualInfoList;
        private Dictionary<PlayerVisualType, PlayerVisualSO> visualInfoDic;

        public PlayerVisualSO this[PlayerVisualType type]
        {
            get { return visualInfoDic[type]; }
        }

        private void OnEnable()
        {
            if(visualInfoDic == null)
            {
                visualInfoDic = new Dictionary<PlayerVisualType, PlayerVisualSO>();

                foreach(PlayerVisualSO visual in visualInfoList)
                {
                    visualInfoDic[visual.VisualType] = visual;
                }
            }
        }
    }
}