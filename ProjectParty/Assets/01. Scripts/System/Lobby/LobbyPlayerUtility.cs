using System;
using OMG.Player;
using UnityEngine;

namespace OMG.Lobbies
{
    public partial class Lobby
    {
        public void ChangePlayerData(ulong clientID, Func<PlayerData, PlayerData> process)
        {
            int playerIndex = GetPlayerData(clientID, out PlayerData playerData);
            if(playerIndex == -1)
                return;

            PlayerData changedData = process.Invoke(playerData);
            players[playerIndex] = changedData;
            players.SetDirty(true);
        }

        public void ForEachPlayer(Action<PlayerData> callback)
        {
            for(int i = 0; i < players.Count; ++i)
                callback.Invoke(players[i]);
        }

        public int GetPlayerData(ulong clientID, out PlayerData playerData)
        {
            playerData = default;

            for(int i = 0; i < players.Count; ++i)
            {
                if(players[i].clientID == clientID)
                {
                    playerData = players[i];
                    return i;
                }
            }

            return -1;
        }
    }
}
