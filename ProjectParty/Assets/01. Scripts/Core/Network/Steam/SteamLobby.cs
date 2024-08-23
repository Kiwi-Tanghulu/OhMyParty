using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;

namespace OMG.Networks
{
    public class SteamLobby : INetworkLobby
    {
        private Lobby lobby;
        public Lobby Lobby => lobby;

        public SteamLobby(Lobby lobby)
        {
            this.lobby = lobby;
        }
            
        public async Task<bool> Join()
        {
            RoomEnter res = await lobby.Join();
            return res == RoomEnter.Success;
        }

        public void Leave()
        {
            lobby.Leave();
        }

        public void Open() => lobby.SetJoinable(true);
        public void Close() => lobby.SetJoinable(false);
    }
}
