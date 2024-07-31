using OMG.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerContainer
{
    private List<PlayerController> playerList;
    public List<PlayerController> PlayerList => playerList;

    public int Count => playerList.Count;

    public PlayerController this[int index] => playerList[index];

    public PlayerContainer()
    {
        playerList = new List<PlayerController>();
    }

    public void RegistPlayer(PlayerController player)
    {
        playerList.Add(player);
    }

    public void UnregistPlayer(PlayerController player)
    {
        playerList.Remove(player);
    }

    public int GetPlayerIndex(PlayerController player)
    {
        return playerList.IndexOf(player);
    }
}
