using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;

namespace OMG.Networks.Steam
{
    public class SteamLobby : INetworkLobby
    {
        private Lobby lobby;
        public Lobby Lobby => lobby;

        public int MemberCount => Lobby.MemberCount;

        public SteamLobby(Lobby lobby)
        {
            this.lobby = lobby;
        }
            
        public async Task<bool> Join()
        {
            RoomEnter res = await Lobby.Join();
            return res == RoomEnter.Success;
        }

        public void Leave()
        {
            Lobby.Leave();
        }

        public void Open() => Lobby.SetJoinable(true);
        public void Close() => Lobby.SetJoinable(false);

        public void SetData(string key, string value) => Lobby.SetData(key, value);
        public string GetData(string key) => Lobby.GetData(key);
            
    }
}
