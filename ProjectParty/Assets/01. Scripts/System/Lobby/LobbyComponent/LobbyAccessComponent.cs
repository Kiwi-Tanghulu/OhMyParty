using OMG.Minigames;
using OMG.Network;

namespace OMG.Lobbies
{
    public class LobbyAccessComponent : LobbyComponent
    {
        private LobbyMinigameComponent minigameComponent = null;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);

            minigameComponent = lobby.GetLobbyComponent<LobbyMinigameComponent>();
            minigameComponent.OnMinigameCycleStartedEvent += HandleMinigameCycleStarted;
            minigameComponent.OnMinigameFinishedEvent += HandleMingameFinished;
        }

        private void HandleMinigameCycleStarted()
        {
            if (IsHost == false)
                return;
            HostManager.Instance.CloseLobby();
        }

        private void HandleMingameFinished(Minigame minigame, bool cycleFinished)
        {
            if (IsHost == false)
                return;

            if (cycleFinished)
                HostManager.Instance.OpenLobby();
        }
    }
}
