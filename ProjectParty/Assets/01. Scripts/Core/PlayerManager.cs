using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private Dictionary<ulong, PlayerController> playerDic = new Dictionary<ulong, PlayerController>();
    public Dictionary<ulong, PlayerController> PlayerDic => playerDic;

    private void Awake()
    {
        Instance = this;
    }
}
