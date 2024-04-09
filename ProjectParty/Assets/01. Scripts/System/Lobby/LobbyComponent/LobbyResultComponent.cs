using OMG.Extensions;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyResultComponent : LobbyComponent
    {
        public void DisplayResult()
        {
            Lobby.PlayerDatas.ForEach(i => {
                Debug.Log($"Player {i.clientID} Score : {i.score}");
            });
        }

        public void DisplayWinner()
        {
            PlayerData winner = new PlayerData(0);
            Lobby.PlayerDatas.ForEach(i => {
                if(i.score >= winner.score)
                    winner = i;
            });

            Debug.Log($"[Result] The Winner is Player {winner.clientID}");
        }
    }
}
