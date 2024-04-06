using OMG.Interacting;
using OMG.Player;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class MinigameSpot : MonoBehaviour, IInteractable
    {
        private LobbyReadyComponent readyComponent = null;
        private LobbyMinigameComponent minigameComponent = null;

        private void Start()
        {
            readyComponent = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            minigameComponent = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
        }

        public bool Interact(Component performer, bool actived, Vector3 point = default)
        {
            if(actived == false)
                return false;

            readyComponent.Ready();
            return true;
        }
    }
}
