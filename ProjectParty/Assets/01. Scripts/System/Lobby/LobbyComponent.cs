using Unity.Netcode;

namespace OMG.Lobbies
{
    public abstract class LobbyComponent : NetworkBehaviour
    {
        public Lobby Lobby { get; private set; } = null;

        public virtual void Init(Lobby lobby)
        {
            Lobby = lobby;
        }
    }
}
