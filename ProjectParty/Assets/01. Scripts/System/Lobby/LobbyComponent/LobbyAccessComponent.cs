using OMG.Inputs;
using OMG.Network;
using OMG.UI.Lobbies;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyAccessComponent : LobbyComponent
    {
        [SerializeField] PlayInputSO input = null;
        [SerializeField] ExitPanel exitPanel = null;
 
        public override void Init(Lobby lobby)
        {
            base.Init(lobby);

            Lobby.OnLobbyStateChangedEvent += HandleLobbyAccess;
            Lobby.OnLobbyStateChangedEvent += HandleExitPanel;
            input.OnEscapeEvent += HandleEscape;            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            input.OnEscapeEvent -= HandleEscape;
        }

        private void HandleEscape()
        {
            if(Lobby.LobbyState != LobbyState.Community)
                return;

            exitPanel.Display(true);
        }

        private void HandleLobbyAccess(LobbyState state)
        {
            if(IsHost == false)
                return;

            if(state == LobbyState.Community)
                HostManager.Instance.OpenLobby();
            else
                HostManager.Instance.CloseLobby();
        }

        private void HandleExitPanel(LobbyState state)
        {
            if(state != LobbyState.Community)
                exitPanel.Display(false);
        }
    }
}
