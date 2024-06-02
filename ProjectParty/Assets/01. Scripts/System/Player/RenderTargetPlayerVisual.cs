using OMG.Lobbies;
using UnityEngine;

namespace OMG.Player
{
    public class RenderTargetPlayerVisual : PlayerVisual
    {
        private ulong ownerID;

        private Animator anim;
        private int isReadyHash = Animator.StringToHash("isReady");

        private RenderTexture renderTexture;
        public RenderTexture RenderTexture => renderTexture;

        [SerializeField] private Camera renderCamera;

        protected override void Start()
        {
            base.Start();

            anim = GetComponent<Animator>();

            CreateRenderTexture();

            Lobby lobby = Lobby.Current;
            lobby.OnLobbyStateChangedEvent += Lobby_OnLobbyStateChangedEvent;
            LobbyReadyComponent lobbyReady = lobby.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        public void SetOwenrID(ulong ownerID)
        {
            this.ownerID = ownerID;
        }

        public void SetReady(bool value)
        {
            anim.SetBool(isReadyHash, value);
        }

        private void CreateRenderTexture()
        {
            RenderTexture rt = new RenderTexture(256, 256, 16);
            rt.Create();
            renderTexture = rt;

            renderCamera.targetTexture = renderTexture;
        }

        private void Lobby_OnLobbyStateChangedEvent(LobbyState state)
        {
            if(state == LobbyState.MinigameFinished)
                SetReady(false);

        }

        private void MinigameInfoUI_OnPlayerReadyEvent(ulong clientID)
        {
            if (Lobby.Current.LobbyState == LobbyState.MinigameSelected)
                SetReady(clientID == ownerID);
        }
    }
}