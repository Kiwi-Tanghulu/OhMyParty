using UnityEngine;

namespace OMG.Lobbies
{
    public enum LobbyState
    {
        None,
        Community, // Play
        MinigameSelecting, // UI
        MinigameSelected, // UI
        MinigamePlaying, // Play
        MinigameFinished // Play
    }
}
