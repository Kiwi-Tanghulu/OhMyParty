using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;

namespace OMG.Networks
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
