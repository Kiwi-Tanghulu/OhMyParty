using OMG.Lobbies;
using OMG.Extensions;

namespace OMG.Player
{
    public class PlayerNameTag : NameTag
    {
        public void SetNameTag(ulong ownerID)
        {
#if STEAMWORKS
            Lobby.Current.PlayerDatas.Find(out PlayerData data, data => data.ClientID == ownerID);

            SetNameTag(data.Name);
#else
            SetNameTag(transform.parent.name);
#endif
        }
    }
}
