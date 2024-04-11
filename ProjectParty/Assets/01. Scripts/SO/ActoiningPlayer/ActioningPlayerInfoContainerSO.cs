using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    [Serializable]
    public class ActioningPlayerInfo
    {
        public ActioningPlayerType PlayerType;
        public PlayerController Prefab;
    }

    [CreateAssetMenu(menuName = "SO/Player/ActioningPlayerSO")]
    public class ActioningPlayerInfoContainerSO : ScriptableObject
    {
        public List<ActioningPlayerInfo> ActioningPlayerInfoList = new List<ActioningPlayerInfo>();

        public ActioningPlayerInfo GetActioningPlayer(ActioningPlayerType type)
        {
            return ActioningPlayerInfoList.Find(x => x.PlayerType == type);
        }
    }
}