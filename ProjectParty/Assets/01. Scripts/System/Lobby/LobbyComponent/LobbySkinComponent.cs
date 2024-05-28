using OMG.Network;
using OMG.Skins;

namespace OMG.Lobbies
{
    public class LobbySkinComponent : LobbyComponent
    {
        private SkinSelector skinSelector = null;

        public override void Init(Lobby lobby)
        {
            base.Init(lobby);
            skinSelector = GetComponent<SkinSelector>();
        }

        private void Start()
        {
            if (IsHost)
                SetSkinData();
            else
                LoadSkinData();

            skinSelector.SetSkin();
        }

        private void SetSkinData()
        {
            string lobbySkin = skinSelector.SkinLibrary.CurrentIndex.ToString();
            ClientManager.Instance.CurrentLobby?.SetData("LobbySkin", lobbySkin);
        }

        private void LoadSkinData()
        {
            if (ClientManager.Instance.CurrentLobby == null)
                return;

            string lobbySkin = ClientManager.Instance.CurrentLobby?.GetData("LobbySkin");
            skinSelector.SkinLibrary.CurrentIndex = int.Parse(lobbySkin);
        }
    }
}