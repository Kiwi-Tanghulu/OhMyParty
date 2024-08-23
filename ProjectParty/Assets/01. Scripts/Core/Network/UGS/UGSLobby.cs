using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

namespace OMG.Networks.UGS
{
    using Lobbies = Unity.Services.Lobbies.Lobbies;

    public class UGSLobby : INetworkLobby
    {
        private Lobby lobby = null;
        public Lobby Lobby => lobby;

        public int MemberCount => Lobby.MaxPlayers - Lobby.AvailableSlots;

        public UGSLobby(Lobby lobby)
        {
            this.lobby = lobby;
        }

        public async Task<bool> Join()
        {
            string roomCode = GetData(DEFINE.JOIN_CODE_KEY);
            JoinAllocation room = await Relay.Instance.JoinAllocationAsync(roomCode);

            RelayServerData relayServer = new RelayServerData(room, "dtls");
            (ClientManager.Instance.NetworkTransport as UnityTransport).SetRelayServerData(relayServer);

            return true;
        }

        public void Leave()
        {
            NetworkManager.Singleton.Shutdown();
        }

        public void Open()
        {
            UpdateLobbyOptions option = new UpdateLobbyOptions() { IsLocked = false };
            UpdateLobby(option);
        }

        public void Close()
        {
            UpdateLobbyOptions option = new UpdateLobbyOptions() { IsLocked = true };
            UpdateLobby(option);
        }

        public string GetData(string key) => Lobby.Data[key].Value;   
        public void SetData(string key, string value)
        {
            Lobby.Data.Add(key, new DataObject(DataObject.VisibilityOptions.Public, value));
            UpdateLobbyOptions option = new UpdateLobbyOptions() { Data = Lobby.Data };
            UpdateLobby(option);
        }

        public async void UpdateLobby(UpdateLobbyOptions option)
        {
            lobby = await Lobbies.Instance.UpdateLobbyAsync(Lobby.Id, option);
        }
    }
}
