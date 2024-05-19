using OMG.Network;
using Steamworks;
using Steamworks.Data;
using TinyGiantStudio.Text;
using UnityEngine;

namespace OMG.UI.Sessions
{
    public class SessionSlot : MonoBehaviour
    {
        private const string OWNER_TEXT_FORMAT = "{0}'S SESSION";
        private const string USER_TEXT_FORMAT = "{0}";

        [SerializeField] Modular3DText ownerText = null;
        [SerializeField] Modular3DText userText = null;

        private Lobby lobby;

        public void Init(Lobby lobby)
        {
            this.lobby = lobby;
            ownerText.Text = string.Format(OWNER_TEXT_FORMAT, lobby.GetData("owner"));
            userText.Text = string.Format(USER_TEXT_FORMAT, lobby.MemberCount);
        }

        public void JoinLobby()
        {
            ClientManager.Instance.JoinLobbyAsync(lobby);
        }
    }
}