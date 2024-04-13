using OMG.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerContainer
{
    private List<PlayerController> playerList;
    public List<PlayerController> PlayerList => playerList;

    public PlayerContainer()
    {
        playerList = new List<PlayerController>(new PlayerController[4]);
    }

    public void RegistPlayer(PlayerController player)
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i] == null)
            {
                playerList[i] = player;
                break;
            }
        }
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
