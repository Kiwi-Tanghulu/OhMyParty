using OMG.Lobbies;
using OMG.Extensions;
using System.Collections;
using UnityEngine;
using Unity.Netcode;

namespace OMG.Player
{
    public class PlayerNameTag : NameTag
    {
        private bool nameSetted;
        private ulong ownerID = 0;

        public void Init(ulong ownerID)
        {
            this.ownerID = ownerID;

            if (Lobby.Current == null)
            {
                SetNameTag("Name Tag");
            }
            else
            {
                TrySetNameTag();
                Lobby.Current.PlayerDatas.OnListChanged += HandlePlayerDatasChanged;
            }
        }

        private void TrySetNameTag()
        {
            int index = Lobby.Current.PlayerDatas.Find(out PlayerData data, data => data.ClientID == ownerID);
            if(index == -1)
                return;

            string nickname = data.Nickname;
            if(string.IsNullOrEmpty(nickname))
                return;

            nameSetted = true;
            SetNameTag(nickname);
        }

        private void HandlePlayerDatasChanged(NetworkListEvent<PlayerData> listEvent)
        {
            TrySetNameTag();

            if(nameSetted)
                Lobby.Current.PlayerDatas.OnListChanged -= HandlePlayerDatasChanged;
        }

        private void OnDestroy()
        {
            if (Lobby.Current != null)
            {
                Lobby.Current.PlayerDatas.OnListChanged -= HandlePlayerDatasChanged;
            }
        }
    }
}
