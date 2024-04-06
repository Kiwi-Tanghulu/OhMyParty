using UnityEngine;

namespace OMG.Lobbies
{
    public abstract class LobbyComponent : MonoBehaviour
    {
        public Lobby Lobby { get; private set; } = null;

        /// <summary>
        /// Called at Lobby::Awake
        /// </summary>
        public virtual void Init(Lobby lobby)
        {
            Lobby = lobby;
        }
    }
}
