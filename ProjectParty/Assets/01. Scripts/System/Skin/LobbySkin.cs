using OMG.Lobbies;
using UnityEngine;

namespace OMG.Skins
{
    public class LobbySkin : Skin
    {
        [SerializeField] MinigameSpot spot = null;

        public override void Init()
        {
            base.Init();
            spot.NetworkObject.TrySetParent(Lobby.Current.NetworkObject);
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
