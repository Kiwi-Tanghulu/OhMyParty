using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

namespace OMG.Networks.Steam
{
    public class SteamHostManager : HostManager
    {
        protected override bool OnBeginStartHost()
        {
            bool response = base.OnBeginStartHost();
            if(response == false)
                return false;

            SteamMatchmaking.OnLobbyCreated += HandleLobbyCreated;
            return true;
        }
        
        protected override async Task<INetworkLobby> CreateLobby(int maxMember)
        {
            Lobby? lobby = await SteamMatchmaking.CreateLobbyAsync(maxMember);
            if(lobby == null)
                return null;

            lobby?.SetData("private", "false");
            lobby?.SetData(DEFINE.OWNER_NAME_KEY, ClientManager.Instance.Nickname);
            
            INetworkLobby networkLobby = new SteamLobby(lobby.Value);
            return networkLobby;
        }

        public override void Disconnect()
        {
            base.Disconnect();
            SteamMatchmaking.OnLobbyCreated -= HandleLobbyCreated;
        }

        private void HandleLobbyCreated(Result result, Lobby lobby)
        {
            if(result != Result.OK)
            {
                Debug.Log($"[Steamworks] Lobby doesn't Created");
                return;
            }

            // 로비가 정상적으로 생성되었다면 각종 설정을 해줌
            lobby.SetPublic();
            lobby.SetJoinable(true);

            // 각 클라이언트가 하나 하나 서버가 됨. P2P이기 때문. 이 때 해당 로비의 서버를 로비를 생성한 오너의 서버로 두겠다는 것
            // Steammatchmaking.OnLobbyGameCreated 가 호출됨
            lobby.SetGameServer(lobby.Owner.Id);
        }
    }
}
