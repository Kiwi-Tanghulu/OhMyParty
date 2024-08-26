using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

namespace OMG.Networks.UGS
{
    using Lobbies = Unity.Services.Lobbies.Lobbies;
    
    public class UGSHostManager : HostManager
    {
        protected override async Task<INetworkLobby> CreateLobby(int maxMember)
        {
            // Unity는 룸과 로비가 별개의 개념임
            // 로비는 룸에 대한 정보만 갖고 있고
            // 릴레이 서버는 룸 자체에 물려있는 것

            Allocation room = await Relay.Instance.CreateAllocationAsync(maxMember);
            string joinCode = await Relay.Instance.GetJoinCodeAsync(room.AllocationId);

            RelayServerData relayServer = new RelayServerData(room, "dtls");
            (ClientManager.Instance.NetworkTransport as UnityTransport).SetRelayServerData(relayServer);

            CreateLobbyOptions lobbyOptions = new CreateLobbyOptions() {
                IsPrivate = false,
                Data = new Dictionary<string, DataObject>() {
                    [DEFINE.JOIN_CODE_KEY] = new DataObject(DataObject.VisibilityOptions.Public, joinCode),
                    [DEFINE.OWNER_NAME_KEY] = new DataObject(DataObject.VisibilityOptions.Public, ClientManager.Instance.Nickname)
                }
            };

            Lobby lobby = await Lobbies.Instance.CreateLobbyAsync(ClientManager.Instance.Nickname, maxMember, lobbyOptions);
            UGSLobby ugsLobby = new UGSLobby(lobby);

            return ugsLobby;
        }
    }
}
