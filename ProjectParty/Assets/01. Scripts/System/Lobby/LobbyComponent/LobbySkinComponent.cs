using OMG.Network;
using OMG.Skins;

namespace OMG.Lobbies
{
    public class LobbySkinComponent : LobbyComponent
    {
        private SkinSelector skinSelector = null;
        private SkinSO skinData = null;

        public LobbySkin Skin => skinSelector.CurrentSkin as LobbySkin;

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

            skinSelector.SetSkin(skinData);
        }

        private void SetSkinData()
        {
            string lobbySkin = skinSelector.SkinLibrary.CurrentIndex.ToString();
            ClientManager.Instance.CurrentLobby?.SetData("LobbySkin", lobbySkin);

            skinData = skinSelector.SkinLibrary.CurrentSkin;
        }

        private void LoadSkinData()
        {
            if (ClientManager.Instance.CurrentLobby == null)
                return;

            string lobbySkin = ClientManager.Instance.CurrentLobby?.GetData("LobbySkin");
            skinData = skinSelector.SkinLibrary.GetSkin(int.Parse(lobbySkin));
        }
    }
}