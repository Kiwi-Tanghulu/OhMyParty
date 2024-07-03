using OMG.Minigames;
using OMG.UI.Lobbies;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyDemoComponent : LobbyComponent
    {
        [SerializeField] DemoPanel demoPanel = null;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);

            LobbyMinigameComponent minigameComponent = lobby.GetLobbyComponent<LobbyMinigameComponent>();
            minigameComponent.OnMinigameFinishedEvent += HandleDemoPanel;
        }

        private void HandleDemoPanel(Minigame minigame, bool cycleFinished)
        {
            if(cycleFinished == false)
                return;

            demoPanel.Display(true);
        }
    }
}
