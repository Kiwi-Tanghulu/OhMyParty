using OMG.Lobbies;
using OMG.Extensions;
using System.Collections;
using UnityEngine;
using Unity.Netcode;

namespace OMG.Player
{
    public class PlayerNameTag : NameTag
    {
        [SerializeField] float updateDelay = 0.5f;
        private bool nameSetted;
        private ulong ownerID = 0;

        public void Init(ulong ownerID)
        {
            this.ownerID = ownerID;

            TrySetNameTag();
            Lobby.Current.PlayerDatas.OnListChanged += HandlePlayerDatasChanged;
            // StartCoroutine(NameTagUpdateRoutine());
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

        // private IEnumerator NameTagUpdateRoutine()
        // {
        //     Debug.Log("ASd");
        //     YieldInstruction delay = new WaitForSeconds(updateDelay);
        //     while(nameSetted == false)
        //     {
        //         yield return delay;
        //         TrySetNameTag();
        //     }
        // }
    }
}
