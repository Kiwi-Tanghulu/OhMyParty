using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

namespace OMG.Network
{
    public class ClientManager
    {
        public static ClientManager Instance = null;

        public Lobby? CurrentLobby = null;

        public ClientManager()
        {
            // 로비에 멤버가 참가하거나 떠났을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyMemberJoined += HandleLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave += HandleLobbyMemberLeave;

            // 누군가가 로비에 나를 초대했을 때 발행되는 이벤트
            SteamMatchmaking.OnLobbyInvite += HandleLobbyInvite;

            // 로비 게임이 생성됐을 때 발행되는 이벤트 (아직 잘 모르겠음)
            SteamMatchmaking.OnLobbyGameCreated += HandleLobbyGameCreated;

            // 친구의 로비에 참가를 요청했을 때 발행되는 이벤트 (요청 응답 처리)
            SteamFriends.OnGameLobbyJoinRequested += HandleLobbyJoinRequested;
        }

        ~ClientManager()
        {
            SteamMatchmaking.OnLobbyMemberJoined -= HandleLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave -= HandleLobbyMemberLeave;

            SteamMatchmaking.OnLobbyInvite -= HandleLobbyInvite;

            SteamMatchmaking.OnLobbyGameCreated -= HandleLobbyGameCreated;

            SteamFriends.OnGameLobbyJoinRequested -= HandleLobbyJoinRequested;
        }

        public async Task<Lobby[]> GetLobbyListAsync(string owner = null, int count = 5)
        {
            LobbyQuery query = SteamMatchmaking.LobbyList
                .WithKeyValue("private", "false")
                .WithKeyValue("closed", "false")
                .WithSlotsAvailable(1)
                .WithMaxResults(count);

            if(string.IsNullOrEmpty(owner) == false)
                query = query.WithKeyValue("owner", owner);

            return await query.RequestAsync();
        }

        public async void JoinLobbyAsync(Lobby lobby)
        {
            if(lobby.GetData("closed") == "true")
                return;

            // 참가 요청에 대한 응답 받기
            RoomEnter reqResult = await lobby.Join();
            if(reqResult != RoomEnter.Success)
            {
                Debug.Log($"[Steamworks] Failed to Join lobby : {reqResult}");
                return;
            }

            // 참가 되었다면 CurrentLobby 업데이트 해줌
            CurrentLobby = lobby;
        }

        #region Steamworks Callback

        private void HandleLobbyMemberJoined(Lobby lobby, Friend friend)
        {
            Debug.Log($"[Steamworks] Member Joined the Lobby : {friend.Name}");
        }

        private void HandleLobbyMemberLeave(Lobby lobby, Friend friend)
        {
            Debug.Log($"[Steamworks] Member Left the Lobby : {friend.Name}");
        }

        private void HandleLobbyInvite(Friend friend, Lobby lobby)
        {
            Debug.Log($"[Steamworks] Lobby Invite From {friend.Name}");
        }

        private void HandleLobbyGameCreated(Lobby lobby, uint ip, ushort port, SteamId id)
        {
            Debug.Log($"[Steamworks] Lobby Game Created");
        }

        private void HandleLobbyJoinRequested(Lobby lobby, SteamId id)
        {
            JoinLobbyAsync(lobby);
        }

        #endregion
    }
}
