using OMG.Extensions;
using OMG.Lobbies;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingLandPlayerController : PlayerController
{
    private int playerIndex;
    public int PlayerIndex => playerIndex;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        playerIndex = Lobby.Current.PlayerDatas.Find(out PlayerData data, data => data.ClientID == OwnerClientId);
    }
}
