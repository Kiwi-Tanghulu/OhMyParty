using System;
using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Network
{
    //이녀석은 인터페이스로 설계하는게 좋아보임. 
    //그래야 스팀 구현체와 UGS구현체중에 번갈아 끼워도 정상적으로 동작할테니까
    //지금 코드는 Steam에 의존적인 것으로 보임.   
    public class ClientManager_
    {
        public static ClientManager_ Instance = null;

        public event Action OnDisconnectEvent = null;
        public Lobby? CurrentLobby = null;
        public ulong ClientID => NetworkManager.Singleton.LocalClientId;

        public ClientManager_()
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

        public void Release()
        {
            SteamMatchmaking.OnLobbyMemberJoined -= HandleLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave -= HandleLobbyMemberLeave;

            SteamMatchmaking.OnLobbyInvite -= HandleLobbyInvite;

            SteamMatchmaking.OnLobbyGameCreated -= HandleLobbyGameCreated;

            SteamFriends.OnGameLobbyJoinRequested -= HandleLobbyJoinRequested;

            OnDisconnectEvent = null;
        }

        public void Disconnect()
        {
            if(NetworkManager.Singleton == null)
                return;
            
            if(HostManager_.Instance.Alive)
                HostManager_.Instance.Disconnect();

            if(GuestManager_.Instance.Alive)
                GuestManager_.Instance.Disconnect();

            OnDisconnectEvent?.Invoke();
        }

        public async Task<Lobby[]> GetLobbyListAsync(string owner = null, int count = 5)
        {
            LobbyQuery query = SteamMatchmaking.LobbyList
                .WithKeyValue("private", "false")
                // .WithKeyValue(DEFINE.LOBBY_CLOSED, "false")
                .WithSlotsAvailable(1)
                .WithMaxResults(count);

            if(string.IsNullOrEmpty(owner) == false)
                query = query.WithKeyValue("owner", owner);

            return await query.RequestAsync();
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
            GuestManager_.Instance.StartGuest(lobby);
        }

        #endregion
    }
}
