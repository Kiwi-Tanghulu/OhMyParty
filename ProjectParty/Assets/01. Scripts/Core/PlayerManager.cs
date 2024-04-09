using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private ActioningPlayerInfoContainerSO actioningPlayerInfoContainer;
    private Dictionary<ulong, Player> playerDic = new Dictionary<ulong, Player>();
    public Dictionary<ulong, Player> PlayerDic => playerDic;

    private void Awake()
    {
        Instance = this;
    }

    public ActioningPlayerInfo GetActioningPlayer(ActioningPlayerType type)
    {
        return actioningPlayerInfoContainer.GetActioningPlayer(type);
    }
}
