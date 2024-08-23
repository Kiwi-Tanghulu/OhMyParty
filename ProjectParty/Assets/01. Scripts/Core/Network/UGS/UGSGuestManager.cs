using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

namespace OMG.Networks.UGS
{
    using Lobbies = Unity.Services.Lobbies.Lobbies;

    public class UGSGuestManager : GuestManager
    {
        public override async Task<INetworkLobby[]> GetLobbyListAsync(int count = 5)
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions {
                Count = count,
                Filters = new List<QueryFilter>() {
                    new QueryFilter(
                        field: QueryFilter.FieldOptions.AvailableSlots,
                        op: QueryFilter.OpOptions.GT,
                        value: "0"
                    )
                },
                Order = new List<QueryOrder>() {
                    new QueryOrder(
                        asc: false,
                        field: QueryOrder.FieldOptions.Created
                    )
                }
            };

            QueryResponse lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);
            if(lobbies == null || lobbies.Results == null || lobbies.Results.Count <= 0)
                return null;

            INetworkLobby[] lobbyList = new INetworkLobby[lobbies.Results.Count];
            for(int i = 0; i < lobbies.Results.Count; ++i)
                lobbyList[i] = new UGSLobby(lobbies.Results[i]);

            return lobbyList;
        }
    }
}
