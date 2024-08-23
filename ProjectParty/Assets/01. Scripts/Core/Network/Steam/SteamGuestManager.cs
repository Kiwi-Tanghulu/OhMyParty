using System.Threading.Tasks;
using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;

namespace OMG.Networks.Steam
{
    public class SteamGuestManager : GuestManager
    {
        public SteamGuestManager()
        {
            SteamFriends.OnGameLobbyJoinRequested += HandleLobbyJoinRequested;
        }

        ~SteamGuestManager()
        {
            SteamFriends.OnGameLobbyJoinRequested -= HandleLobbyJoinRequested;
        }

        public override async Task<INetworkLobby[]> GetLobbyListAsync(int count = 5)
        {
            LobbyQuery query = SteamMatchmaking.LobbyList
                .WithKeyValue("private", "false")
                .WithSlotsAvailable(1)
                .WithMaxResults(5);

            Lobby[] lobbies = await query.RequestAsync();
            if(lobbies == null)
                return null;

            INetworkLobby[] lobbyList = new INetworkLobby[lobbies.Length];
            for(int i = 0; i < lobbies.Length; ++i)
                lobbyList[i] = new SteamLobby(lobbies[i]);
            
            return lobbyList;
        }

        protected override bool OnGuestStarted(INetworkLobby networkLobby)
        {
            bool response = base.OnGuestStarted(networkLobby);
            if(response == false)
                return false;

            FacepunchTransport transport = ClientManager.Instance.NetworkTransport as FacepunchTransport;
            transport.targetSteamId = (networkLobby as SteamLobby).Lobby.Owner.Id;

            return true;
        }

        private void HandleLobbyJoinRequested(Lobby lobby, SteamId id)
        {
            SteamLobby steamLobby = new SteamLobby(lobby);
            StartGuest(steamLobby);
        }
    }
}
