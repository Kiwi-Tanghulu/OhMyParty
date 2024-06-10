using OMG.Extensions;
using OMG.Lobbies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerNameTag : NameTag
    {
        public void SetNameTag(ulong ownerID)
        {
            //Lobby.Current.PlayerDatas.Find(out PlayerData data, data => data.clientID == ownerID);

            //SetNameTag(data.name);

            SetNameTag(transform.parent.name);
        }
    }
}
